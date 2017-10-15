using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.Services.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Services.Shared
{
    public class FileManagerTests
    {
        private readonly FileManager sut;
        private IHostingEnvironment _environment;
        private IFormFile _formFile;
        private IFileSystemWrapper _fileSystem;
        private IPathWrapper _pathWrapper;
        private IFileWrapper _fileWrapper;
        private IDirectoryWrapper _directoryWrapper;

        public FileManagerTests()
        {
            _environment = A.Fake<IHostingEnvironment>();
            _formFile = A.Fake<IFormFile>();
            _fileSystem = A.Fake<IFileSystemWrapper>();
            _pathWrapper = A.Fake<IPathWrapper>();
            _fileWrapper = A.Fake<IFileWrapper>();
            _directoryWrapper = A.Fake<IDirectoryWrapper>();

            A.CallTo(() => _fileSystem.File).Returns(_fileWrapper);
            A.CallTo(() => _fileSystem.Directory).Returns(_directoryWrapper);
            A.CallTo(() => _fileSystem.Path).Returns(_pathWrapper);

            sut = new FileManager(_environment, _fileSystem);
        }

        private MemoryStream FakeMemoryStream()
        {
            var fileContent = "Hello World from a Fake File";

            var memoryStream = new MemoryStream();

            var writer = new StreamWriter(memoryStream);

            writer.Write(fileContent);

            writer.Flush();

            memoryStream.Position = 0;

            return memoryStream;
        }

        private void StubUploadFileAsyncDependency()
        {
            var memoryStream = FakeMemoryStream();

            A.CallTo(() => _formFile.OpenReadStream()).Returns(memoryStream);
            A.CallTo(() => _formFile.Length).Returns(256);
            A.CallTo(() => _formFile.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            A.CallTo(() => _pathWrapper.GetFileNameWithoutExtension(A<string>._)).Returns("dummy-file-name");
            A.CallTo(() => _pathWrapper.GetExtension(A<string>._)).Returns(".txt");
            A.CallTo(() => _pathWrapper.Combine(A<string[]>._)).Returns("C:\\UnitTestFolder");
            A.CallTo(() => _pathWrapper.Combine(A<string>._, A<string>._)).Returns("C:\\UnitTestFolder\\dummy-file-name.txt");

            A.CallTo(() => _environment.WebRootPath).Returns("C:\\");
        }

        [Fact]
        public void DeleteFile_FileNameArgumentIsNullOrEmpty_ThrowArgumentNullException()
        {
            //Act
            Action deleteFile = () => sut.DeleteFile(null, new List<string>());

            //Assert
            deleteFile.ShouldThrow<ArgumentNullException>()
                .WithMessage("*The fileName argument cannot be null, empty or whitespace.*")
                .And.ParamName.Should()
                .Be("fileName");

            //you could also write like this which is more readable in my opinion
            sut.Invoking(s => s.DeleteFile("", new List<string>()))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("*The fileName argument cannot be null, empty or whitespace.*")
                .And.ParamName.Should()
                .Be("fileName");
        }

        [Fact]
        public void DeleteFile_PathArgumentIsNullOrEmpty_ThrowArgumentException()
        {
            //Act
            Action deleteFile = () => sut.DeleteFile("dummy-file-name.jpg", null);

            //Assert
            deleteFile.ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");

            //you could also write like this which is more readable in my opinion
            sut.Invoking(s => s.DeleteFile("dummy-file-name.jpg", new List<string>()))
                .ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");
        }

        [Fact]
        public void DeleteFile_PathToFileDoesNotExist_ReturnFileStatusFileNotExist()
        {
            A.CallTo(() => _environment.WebRootPath).Returns("C:\\");

            A.CallTo(() => _fileWrapper.Exists(A<string>._)).Returns(false);

            //Act
            var result = sut.DeleteFile("dummy-file-name.jpg", new List<string> { "dummyPath1", "dummyPath2" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.FileNotExist);
            A.CallTo(() => _pathWrapper.Combine(A<string[]>._)).MustHaveHappened(Repeated.AtLeast.Once);
            A.CallTo(() => _fileWrapper.Exists(A<string>._)).MustHaveHappened(Repeated.AtLeast.Once);
        }

        [Fact]
        public void DeleteFile_FileExistAndGotDeleted_ReturnFileStatusDeleteSuccess()
        {
            A.CallTo(() => _fileWrapper.Exists(A<string>._)).ReturnsNextFromSequence(true, false);

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteSuccess);
            A.CallTo(() => _pathWrapper.Combine(A<string[]>._)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => _fileWrapper.Exists(A<string>._)).MustHaveHappened(Repeated.Exactly.Twice);
            A.CallTo(() => _fileWrapper.Delete(A<string>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void DeleteFile_FileExistAndNotDeleted_ReturnFileStatusDeleteFailed()
        {
            A.CallTo(() => _fileWrapper.Exists(A<string>._)).ReturnsNextFromSequence(true, true);

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteFailed);
        }

        [Fact]
        public void DeleteFile_FileExistAndNotDeleted_ExistCalledTwice()
        {
            A.CallTo(() => _fileWrapper.Exists(A<string>._)).ReturnsNextFromSequence(true, true);

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            A.CallTo(() => _fileWrapper.Exists(A<string>._)).MustHaveHappened(Repeated.Exactly.Twice);
        }

        [Fact]
        public void UploadFileAsync_FileIsNullThrowArgumentNullException()
        {
            //Act
            Func<Task> uploadFile = () => sut.UploadFileAsync(null, new List<string>());

            //Assert
            uploadFile.ShouldThrow<ArgumentNullException>()
                .WithMessage("*The file argument cannot be null.*")
                .And.ParamName.Should()
                .Be("file");
        }

        [Fact]
        public void UploadFileAsync_PathIsNullOrEmpty_ThrowArgumentException()
        {
            //Act
            Func<Task> uploadFile = () => sut.UploadFileAsync(_formFile, new List<string>());

            //Assert
            uploadFile.ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");
        }

        [Fact]
        public async Task UploadFileAsync_FileLengthIsZero_ReturnNull()
        {
            A.CallTo(() => _formFile.Length).Returns(0);

            //Act
            var result = await sut.UploadFileAsync(_formFile, new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UploadFileAsync_FileLengthIsNotZero_PassesTheCorrectPathToCombine()
        {
            StubUploadFileAsyncDependency();

            var path = new List<string> {"UnitTestFolder", "InnerFolder"}.ToList();

            var result = await sut.UploadFileAsync(_formFile, path);

            A.CallTo(() => _fileSystem.Path.Combine(A<string[]>.That.Matches(a=> string.Join(",", a) == string.Join(",", path)))).MustHaveHappened();
        }

        [Fact]
        public async Task UploadFileAsync_DirectoryNotExist_CreateDirectoryCalled()
        {
            StubUploadFileAsyncDependency();
            A.CallTo(() => _fileSystem.Directory.Exists(A<string>._)).Returns(false);

            var path = new List<string> {"UnitTestFolder", "InnerFolder"}.ToList();

            var result = await sut.UploadFileAsync(_formFile, path);

            A.CallTo(() => _fileSystem.Directory.CreateDirectory(A<string>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public async Task UploadFileAsync_Always_ReturnsCorrectFileName()
        {
            StubUploadFileAsyncDependency();

            var path = new List<string> {"UnitTestFolder", "InnerFolder"}.ToList();
            A.CallTo(() => _pathWrapper.GetFileNameWithoutExtension(A<string>._)).Returns("super-duper-file-name");

            var result = await sut.UploadFileAsync(_formFile, path);

            result.Should().NotBeNull();
            result.Should().Contain("super-duper-file-name");
        }
    }
}