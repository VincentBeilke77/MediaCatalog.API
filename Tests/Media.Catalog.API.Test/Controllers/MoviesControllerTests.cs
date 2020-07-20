using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediaCatalog.API.Controllers;
using MediaCatalog.API.Data;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace Media.Catalog.API.Test.Controllers
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepo;
        private readonly Mock<IActorRepository> _mockActorRepo;
        private readonly Mock<IDirectorRepository> _mockDirectorRepo;
        private readonly Mock<IStudioRepository> _mockStudioRepo;
        private readonly Mock<IRatingRepository> _mockRatingRepo;
        private readonly Mock<IGenreRepository> _mockGenreRepo;

        private readonly IMapper _mapper;
        private readonly Mock<LinkGenerator> _linkGenerator;

        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockMovieRepo = new Mock<IMovieRepository>();
            _mockActorRepo = new Mock<IActorRepository>();
            _mockDirectorRepo = new Mock<IDirectorRepository>();
            _mockStudioRepo = new Mock<IStudioRepository>();
            _mockRatingRepo = new Mock<IRatingRepository>();
            _mockGenreRepo = new Mock<IGenreRepository>();

            var mcProfile = new MediaCatalogProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mcProfile));
            _mapper = new Mapper(configuration);

            _linkGenerator = new Mock<LinkGenerator>();

            _controller = new MoviesController(_mockMovieRepo.Object, _mockRatingRepo.Object, _mockGenreRepo.Object,
                _mockActorRepo.Object, _mockDirectorRepo.Object, _mockStudioRepo.Object, _mapper,
                _linkGenerator.Object);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsActionResultMovieModel()
        {
            //-- Arrange

            //-- Act
            var result = await _controller.Get();

            //-- Assert
            Assert.IsType<ActionResult<MovieModel[]>>(result);
        }

        [Fact]
        public async void Get_ActionExecutes_ReturnsOkObjectResult()
        {
            //-- Arrange
            _mockMovieRepo.Setup(repo => repo.GetAllMoviesAsync())
                .Returns(Task.FromResult(
                    new[]
                    {
                        new Movie {Title = "Movie 1"},
                        new Movie {Title = "Movie 2"}
                    }));

            //-- Act
            var action = await _controller.Get();
            var result = action.Result;

            //-- Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get_ActionExecutes_ReturnsExactNumberOfMovies()
        {
            //-- Arrange
            _mockMovieRepo.Setup(repo => repo.GetAllMoviesAsync())
                .Returns(Task.FromResult(
                    new[]
                    {
                        new Movie {Title = "Movie 1"},
                        new Movie {Title = "Movie 2"}
                    }));
            var expected = 2;

            //-- Act
            var action = await _controller.Get();
            var result = action.Result as OkObjectResult;

            //-- Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsType<MovieModel[]>(okResult.Value);
            Assert.Equal(expected, actual.Length);
        }

        [Fact]
        public async void Get_ActionExecutes_ReturnsRequestedProduct()
        {
            //-- Arrange
            _mockMovieRepo.Setup(repo => repo.GetMovieAsync(10))
                .Returns(Task.FromResult(
                    new Movie { Id = 10, Title = "Movie 1" }));

            var expected = new MovieModel { Id = 10, Title = "Movie 1" };

            //-- Act
            var result = await _controller.Get(10);

            //-- Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actual = Assert.IsType<MovieModel>(okResult.Value);
            Assert.Equal(expected.Id, actual.Id);
        }
    }
}