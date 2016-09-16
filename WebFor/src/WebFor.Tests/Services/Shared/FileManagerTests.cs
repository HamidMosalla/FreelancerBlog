using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;
using WebFor.Infrastructure.Services.Shared;
using FluentAssertions;
using WebFor.Core.Enums;

namespace WebFor.Tests.Services.Shared
{
    public class FileManagerTests : IDisposable
    {
        private Mock<IHostingEnvironment> _environment;

        public FileManagerTests()
        {
            if (!Directory.Exists("C:\\UnitTestFolder"))
            {
                Directory.CreateDirectory("C:\\UnitTestFolder");
            }

            _environment = new Mock<IHostingEnvironment>();
        }

        public void Dispose()
        {
            if (Directory.Exists("C:\\UnitTestFolder"))
            {
                Directory.Delete("C:\\UnitTestFolder");
            }
        }

        [Fact]
        public void DeleteFile_ShouldThrowArgumentNullException_IfFileNameArgumentIsNullOrEmpty()
        {
            //Arrange
            var sut = new FileManager(_environment.Object);

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
            var sut = new FileManager(_environment.Object);

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
            var sut = new FileManager(_environment.Object);

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            //Act
            var result = sut.DeleteFile("dummy-file-name.jpg", new List<string> { "dummyPath1", "dummyPath2" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.FileNotExist);
        }

        [Fact]
        public void DeleteFile_ShouldReturnFileStatusDeleteSuccess_IfFileExistAndGotDeleted()
        {
            //Arrange
            var sut = new FileManager(_environment.Object);

            _environment.SetupGet(e => e.WebRootPath).Returns("C:\\");

            var stream = File.Create("C:\\UnitTestFolder\\TestFile.txt");
            stream.Dispose();

            //Act
            var result = sut.DeleteFile("TestFile.txt", new List<string> { "UnitTestFolder" });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FileStatus>();
            result.Should().Be(FileStatus.DeleteSuccess);
            File.Exists("C:\\UnitTestFolder\\TestFile.txt").Should().BeFalse();
        }

        [Fact]
        public void UploadFile_ShouldThrowArgumentNullException_IfFileIsNull()
        {
            //Arrange
            var sut = new FileManager(_environment.Object);

            //Act
            Func<Task> uploadFile = () => sut.UploadFile(null, new List<string>());

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
    }
}
