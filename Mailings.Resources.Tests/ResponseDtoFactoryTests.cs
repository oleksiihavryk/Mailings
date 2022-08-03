using System;
using Mailings.Resources.API.Dto;
using Mailings.Resources.API.Exceptions;
using Mailings.Resources.Tests.Comparer;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Mailings.Resources.Tests;
public class ResponseDtoFactoryTests
{
    private readonly IResponseDtoComparer _comparer = new ResponseDtoComparer();

    [Fact]
    public void ResponseDtoFactoryTests_ReturningCorrectEmptySuccessResponse()
    {
        //arrange
        var responseFactory = new ResponseFactory();
        //act
        var emptySuccess = responseFactory.EmptySuccess;
        //assert
        Assert.Equal(new ResponseDto()
        {
            IsSuccess = true,
            Messages = Array.Empty<string>(),
            Result = null,
            StatusCode = StatusCodes.Status204NoContent
        }, emptySuccess, _comparer);
    }
    [Fact]
    public void ResponseDtoFactoryTests_ReturningCorrectEmptyFailedResponse()
    {
        //arrange
        var responseFactory = new ResponseFactory();
        //act
        var emptyFailed = responseFactory.EmptyInternalServerError;
        //assert
        Assert.Equal(new ResponseDto()
        {
            IsSuccess = false,
            Messages = Array.Empty<string>(),
            Result = null,
            StatusCode = StatusCodes.Status500InternalServerError
        }, emptyFailed, _comparer);
    }
    [Theory]
    [InlineData("i love paris")]
    [InlineData(11, SuccessResponseType.Ok)]
    [InlineData(null, SuccessResponseType.MissingResult)]
    public void ResponseDtoFactoryTests_CreatingSuccessResponse(
        object result,
        SuccessResponseType successType = SuccessResponseType.Unknown)
    {
        //arrange
        var responseFactory = new ResponseFactory();
        //act and assert
        if (successType == SuccessResponseType.Unknown)
        {
            Assert.Throws<UnknownResponseTypeException>(() =>
            {
                responseFactory.CreateSuccess(successType, result);
            });
        }
        else
        {
            var successResult = responseFactory
                .CreateSuccess(successType, result);

            Assert.Equal(new ResponseDto()
            {
                IsSuccess = true,
                Messages = Array.Empty<string>(),
                Result = result,
                StatusCode = successType switch
                {
                    SuccessResponseType.Ok => StatusCodes.Status200OK,
                    SuccessResponseType.MissingResult => StatusCodes.Status204NoContent,
                    _ => throw new InvalidOperationException(
                        "Impossible exception if test and program working correct")
                }
            }, successResult, _comparer);
        }
    }
    [Theory]
    [InlineData(
        "i definitely love paris",
        FailedResponseType.BadRequest)]
    [InlineData(
        "11",
        FailedResponseType.NotFound)]
    [InlineData("")]
    public void ResponseDtoFactoryTests_CreatingFailedResponseWithSingleMessage(
        string message,
        FailedResponseType failedType = FailedResponseType.Unknown)
    {
        //arrange
        var responseFactory = new ResponseFactory();
        //act and assert
        if (failedType == FailedResponseType.Unknown)
        {
            Assert.Throws<UnknownResponseTypeException>(() =>
            {
                responseFactory.CreateFailedResponse(failedType, message);
            });
        }
        else
        {
            var failedResult = responseFactory
                .CreateFailedResponse(failedType, message);
            Assert.Equal(new ResponseDto
            {
                IsSuccess = false,
                Messages = new[] { message },
                Result = null,
                StatusCode = failedType switch
                {
                    FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                    FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                    _ => throw new InvalidOperationException(
                        "Impossible exception if test and program works well")
                }
            }, failedResult, _comparer);
        }
    }
    [Theory]
    [InlineData(
        new [] { "i definitely love paris" },
        FailedResponseType.BadRequest)]
    [InlineData(
        new [] { "11" },
        FailedResponseType.NotFound)]
    [InlineData(
        new[] { "" },
        FailedResponseType.NotFound)]
    public void ResponseDtoFactoryTests_CreatingFailedResponseWithMultipleMessages(
        string[] messages,
        FailedResponseType failedType = FailedResponseType.Unknown)
    {
        //arrange
        var responseFactory = new ResponseFactory();
        //act and assert
        if (failedType == FailedResponseType.Unknown)
        {
            Assert.Throws<UnknownResponseTypeException>(() =>
            {
                responseFactory.CreateFailedResponse(failedType, messages);
            });
        }
        else
        {
            var failedResult = responseFactory
                .CreateFailedResponse(failedType, messages);
            Assert.Equal(new ResponseDto
            {
                IsSuccess = false,
                Messages = messages,
                Result = null,
                StatusCode = failedType switch
                {
                    FailedResponseType.BadRequest => StatusCodes.Status400BadRequest,
                    FailedResponseType.NotFound => StatusCodes.Status404NotFound,
                    _ => throw new InvalidOperationException(
                        "Impossible exception if test and program works well")
                }
            }, failedResult, _comparer);
        }
    }
}