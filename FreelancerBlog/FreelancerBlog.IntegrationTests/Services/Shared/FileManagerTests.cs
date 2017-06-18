using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Infrastructure.Services.Shared;
using FreelancerBlog.Infrastructure.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using WebFor.Infrastructure.Wrappers;
using Xunit;

namespace FreelancerBlog.IntegrationTests.Services.Shared
{
    public class FileManagerTests : IDisposable
    {
        private  Mock<IHostingEnvironment> _environment;
        private  Mock<IFormFile> _formFile;
        private  FileWrapper _fileWrapper;
        private  DirectoryWrapper _directoryWrapper;
        private  PathWrapper _pathWrapper;
        private  FileSystemWrapper _fileSystemWrapper;
        private  FileManager _sut;

        public FileManagerTests()
        {
            _environment = new Mock<IHostingEnvironment>();
            _formFile = new Mock<IFormFile>();

            _pathWrapper = new PathWrapper();
            _directoryWrapper = new DirectoryWrapper();
            _fileWrapper = new FileWrapper();
            _fileSystemWrapper = new FileSystemWrapper(_fileWrapper, _directoryWrapper, _pathWrapper);
            _sut = new FileManager(_environment.Object, _fileSystemWrapper);

            if (!Directory.Exists("C:\\UnitTestFolder"))
            {
                Directory.CreateDirectory("C:\\UnitTestFolder");
            }
        }

        public void Dispose()
        {
            var directoryPath = "C:\\UnitTestFolder";

            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath);
            }
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
            //In the constructor

            //Act
            Action deleteFile = () => _sut.DeleteFile(null, new List<string>());

            //Assert
            deleteFile.ShouldThrow<ArgumentNullException>()
                .WithMessage("*The fileName argument cannot be null, empty or whitespace.*")
                .And.ParamName.Should()
                .Be("fileName");

            //you could also write like this which is more readable in my opinion
            _sut.Invoking(s => s.DeleteFile("", new List<string>()))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("*The fileName argument cannot be null, empty or whitespace.*")
                .And.ParamName.Should()
                .Be("fileName");
        }

        [Fact]
        public void DeleteFile_ShouldThrowArgumentException_IfPathArgumentIsNullOrEmpty()
        {
            //Arrange In constructor

            //Act
            Action deleteFile = () => _sut.DeleteFile("dummy-file-name.jpg", null);

            //Assert
            deleteFile.ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");

            //you could also write like this which is more readable in my opinion
            _sut.Invoking(s => s.DeleteFile("dummy-file-name.jpg", new List<string>()))
                .ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");
        }

        [Fact]
        public void DeleteFile_ShouldReturnFileStatusFileNotExist_IfPathToFileDoesNotExist()
        {
            //Arrange In constructor

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = _sut.DeleteFile("dummy-file-name.jpg", new List<string> { "dummyPath1", "dummyPath2" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.FileNotExist);
        }

        [Fact]
        public void DeleteFile_ShouldReturnFileStatusDeleteSuccess_IfFileExistAndGotDeleted()
        {
            //Arrange In constructor

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            var stream = File.Create("C:\\UnitTestFolder\\TestFile.txt");
            stream.Dispose();

            //Act
            var result = _sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteSuccess);
            File.Exists("C:\\UnitTestFolder\\TestFile.txt").Should().BeFalse();
        }



        [Fact]
        public void UploadFile_ShouldThrowArgumentNullException_IfFileIsNull()
        {
            //Arrange In constructor

            //Act
            Func<Task> uploadFile = () => _sut.UploadFileAsync(null, new List<string>());

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
            //Arrange In constructor

            //Act
            Func<Task> uploadFile = () => _sut.UploadFileAsync(_formFile.Object, new List<string>());

            //Assert
            uploadFile.ShouldThrow<ArgumentException>()
                .WithMessage("*The path argument cannot be null or empty.*")
                .And.ParamName.Should()
                .Be("path");
        }

        [Fact]
        public async Task UploadFile_ShouldReturnNull_IfFileLengthIsZero()
        {
            //Arrange In constructor

            _formFile.SetupGet(f => f.Length).Returns(0);

            //Act
            var result = await _sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UploadFile_ShouldReturnResult_WithTheCorrectFileName()
        {
            //Arrange In constructor

            var memoryStream = FakeMemoryStream();

            _formFile.Setup(m => m.OpenReadStream()).Returns(memoryStream);

            _formFile.SetupGet(f => f.Length).Returns(256);

            _formFile.SetupGet(f => f.ContentDisposition).Returns("form-data; name=\"UserAvatarFile\"; filename=\"TestFile.txt\"");

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = await _sut.UploadFileAsync(_formFile.Object, new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().StartWith("TestFile");
            File.Exists($"C:\\UnitTestFolder\\{result}").Should().BeTrue();
        }


    }
}
