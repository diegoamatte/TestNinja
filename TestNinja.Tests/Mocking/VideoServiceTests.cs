using Moq;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.Tests.Mocking
{
    public class VideoServiceTests
    {
        private Mock<IVideoRepository> _repository;
        private Mock<IFileReader> _fileReader;
        private VideoService _videoService;

        public VideoServiceTests()
        {
            _repository = new Mock<IVideoRepository>();
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_ReturnsEmpty_WhenNoUnprocessedVideos()
        {
            //Arrange
            _repository.Setup(vr => vr.GetUnprocessedVideos())
                .Returns(new List<Video>().AsEnumerable());
            
            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetUnprocessedVideosAsCsv_ReturnsIds_WhenUnprocessedVideos()
        {
            //Arrange
            _repository.Setup(vr => vr.GetUnprocessedVideos())
                .Returns(new List<Video>()
                 {
                    new Video(){ Id = 1 },
                    new Video(){ Id = 2 },
                    new Video(){ Id = 3 },
                 });

            //Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            //Assert
            Assert.Equal("1,2,3", result);
        }
    }
}
