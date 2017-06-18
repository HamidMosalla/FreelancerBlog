using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.Infrastructure.Services.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FreelancerBlog.UnitTests.Services.Shared
{
    public class FileManagerTests
    {
        private Mock<IHostingEnvironment> _environment;
        private Mock<IFormFile> _formFile;
        private Mock<IFileSystemWrapper> _fileSystem;
        private Mock<IPathWrapper> _pathWrapper;
        private Mock<IFileWrapper> _fileWrapper;
        private Mock<IDirectoryWrapper> _directoryWrapper;

        public FileManagerTests()
        {
            _environment = new Mock<IHostingEnvironment>();
            _formFile = new Mock<IFormFile>();
            _fileSystem = new Mock<IFileSystemWrapper>();
            _pathWrapper = new Mock<IPathWrapper>();
            _fileWrapper = new Mock<IFileWrapper>();
            _directoryWrapper = new Mock<IDirectoryWrapper>();

            _fileSystem.SetupGet<IFileWrapper>(f => f.File).Returns(_fileWrapper.Object);
            _fileSystem.SetupGet<IDirectoryWrapper>(f => f.Directory).Returns(_directoryWrapper.Object);
            _fileSystem.SetupGet<IPathWrapper>(f => f.Path).Returns(_pathWrapper.Object);

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




        [Fact]
        public void DeleteFile_ShouldThrowArgumentNullException_IfFileNameArgumentIsNullOrEmpty()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

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
        public void DeleteFile_ShouldThrowArgumentException_IfPathArgumentIsNullOrEmpty()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

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
        public void DeleteFile_ShouldReturnFileStatusFileNotExist_IfPathToFileDoesNotExist()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            _fileWrapper.Setup(f => f.Exists(It.IsAny<string>())).Returns(false);

            //Act
            var result = sut.DeleteFile("dummy-file-name.jpg", new List<string> { "dummyPath1", "dummyPath2" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.FileNotExist);
            _pathWrapper.Verify(p => p.Combine(It.IsAny<string[]>()), Times.Once);
            _fileWrapper.Verify(f => f.Exists(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteFile_ShouldReturnFileStatusDeleteSuccess_IfFileExistAndGotDeleted()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            _fileWrapper.SetupSequence<IFileWrapper, bool>(f => f.Exists(It.IsAny<string>())).Returns(true).Returns(false);

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteSuccess);
            _pathWrapper.Verify(p => p.Combine(It.IsAny<string[]>()), Times.Once);
            _fileWrapper.Verify(f => f.Exists(It.IsAny<string>()), Times.Exactly(2));
            _fileWrapper.Verify(f => f.Delete(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteFile_ShouldReturnFileStatusDeleteFailed_IfFileExistAndNotDeleted()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            _fileWrapper.SetupSequence<IFileWrapper, bool>(f => f.Exists(It.IsAny<string>())).Returns(true).Returns(true);

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteFailed);
            _pathWrapper.Verify(p => p.Combine(It.IsAny<string[]>()), Times.Once);
            _fileWrapper.Verify(f => f.Exists(It.IsAny<string>()), Times.Exactly(2));
            _fileWrapper.Verify(f => f.Delete(It.IsAny<string>()), Times.Once);
        }





        [Fact]
        public void UploadFile_ShouldThrowArgumentNullException_IfFileIsNull()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            //Act
            Func<Task> uploadFile = () => sut.UploadFileAsync(null, new List<string>());

            //Assert
            uploadFile.ShouldThrow<ArgumentNullException>()
                .WithMessage("*The file argument cannot be null.*")
                .And.ParamName.Should()
                .Be("file");

            //you could use this instead of above, here we use `Awaiting` instead of `Invoking`
            //sut.Awaiting(s => s.UploadFile(null, new List<string>()))
            //    .ShouldThrow<ArgumentNullException>()
            //    .WithMessage("*The file argument cannot be null.*")
            //    .And.ParamName.Should()
            //    .Be("file");
        }

        [Fact]
        public void UploadFile_ShouldThrowArgumentException_IfPathIsNullOrEmpty()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            //Act
            Func<Task> uploadFile = () => sut.UploadFileAsync(_formFile.Object, new List<string>());

            //Assert
            uploadFile.ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");
        }

        [Fact]
        public async Task UploadFile_ShouldReturnNull_IfFileLengthIsZero()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            _formFile.SetupGet(f => f.Length).Returns(0);

            //Act
            var result = await sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task UploadFile_ShouldCallPathMethods_SpecificNumberOfTimes()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            var memoryStream = FakeMemoryStream();

            _formFile.Setup(m => m.OpenReadStream()).Returns(memoryStream);

            _formFile.SetupGet(f => f.Length).Returns(256);

            _pathWrapper.Setup(p => p.GetFileNameWithoutExtension(It.IsAny<string>())).Returns("dummy-file-name");

            _pathWrapper.Setup(p => p.GetExtension(It.IsAny<string>())).Returns(".txt");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string[]>())).Returns("C:\\UnitTestFolder");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string>(), It.IsAny<string>())).Returns("C:\\UnitTestFolder\\dummy-file-name.txt");

            _formFile.SetupGet(f => f.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = await sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            _pathWrapper.Verify(p => p.GetFileNameWithoutExtension(It.IsAny<string>()), Times.Once);
            _pathWrapper.Verify(p => p.GetExtension(It.IsAny<string>()), Times.Once);
            _pathWrapper.Verify(p => p.Combine(It.IsAny<string[]>()), Times.Exactly(2));
        }

        [Fact]
        public async Task UploadFile_ShouldCallDirectoryMethods_ExactlyOneTime()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            var memoryStream = FakeMemoryStream();

            _formFile.Setup(m => m.OpenReadStream()).Returns(memoryStream);

            _formFile.SetupGet(f => f.Length).Returns(256);

            _pathWrapper.Setup(p => p.GetFileNameWithoutExtension(It.IsAny<string>())).Returns("dummy-file-name");

            _pathWrapper.Setup(p => p.GetExtension(It.IsAny<string>())).Returns(".txt");

            _formFile.SetupGet(f => f.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            _directoryWrapper.Setup(d => d.Exists(It.IsAny<string>())).Returns(false);

            //Act
            var result = await sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            _directoryWrapper.Verify(d => d.Exists(It.IsAny<string>()), Times.Once);
            _directoryWrapper.Verify(d => d.CreateDirectory(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task UploadFile_ShouldCallSaveAsAsyncOnFile_Once()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            var memoryStream = FakeMemoryStream();

            _formFile.Setup(m => m.OpenReadStream()).Returns(memoryStream);

            _formFile.SetupGet(f => f.Length).Returns(256);

            _pathWrapper.Setup(p => p.GetFileNameWithoutExtension(It.IsAny<string>())).Returns("dummy-file-name");

            _pathWrapper.Setup(p => p.GetExtension(It.IsAny<string>())).Returns(".txt");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string[]>())).Returns("C:\\UnitTestFolder");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string>(), It.IsAny<string>())).Returns("C:\\UnitTestFolder\\dummy-file-name.txt");

            _formFile.SetupGet(f => f.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = await sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            _fileWrapper.Verify(f => f.SaveAsAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UploadFile_ShouldReturnResult_WithTheCorrectFileName()
        {
            //Arrange
            var sut = new FileManager(_environment.Object, _fileSystem.Object);

            var memoryStream = FakeMemoryStream();

            _formFile.Setup(m => m.OpenReadStream()).Returns(memoryStream);

            _formFile.SetupGet(f => f.Length).Returns(256);

            _pathWrapper.Setup(p => p.GetFileNameWithoutExtension(It.IsAny<string>())).Returns("dummy-file-name");

            _pathWrapper.Setup(p => p.GetExtension(It.IsAny<string>())).Returns(".txt");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string[]>())).Returns("C:\\UnitTestFolder");

            _pathWrapper.Setup(p => p.Combine(It.IsAny<string>(), It.IsAny<string>())).Returns("C:\\UnitTestFolder\\dummy-file-name.txt");

            _formFile.SetupGet(f => f.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = await sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().StartWith("dummy");
        }

    }
}
