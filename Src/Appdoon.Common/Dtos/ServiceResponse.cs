using System;
using System.Collections.Generic;

namespace Mapdoon.Common.Dtos;

public class ServiceResponse
{
	public int StatusCode { get; set; }
	public string Message { get; set; }
	public Guid? RequestId { get; set; }
	public ServiceException Exception { get; set; }
}

public class ServiceException
{
	public string Type { get; set; }
	public string Message { get; set; }
	public string StackTrace { get; set; }
}

public enum ResponseStatusEnum
{
	Success = 200,
	Error = 400,
	Warning = 401,
	NotFound = 404,
	Unauthorized = 401,
	TimeOut = 408
}

public enum ResponseSubStatusEnum
{
	/// <summary>
	/// StatusCode ==> 300,....
	/// </summary>
	ETC = 0,
	/// <summary>
	/// StatusCode ==> 200
	/// Color = GREEN
	/// </summary>
	Success = 2,
	/// <summary>
	/// StatusCode ==> 100
	/// </summary>
	/// Color = BLUE
	Info = 1,
	/// <summary>
	/// StatusCode ==> 500
	/// </summary>
	/// Color = RED
	Error = 4,
	/// <summary>
	/// StatusCode ==> 400
	/// </summary>
	/// Color= YELLOW
	Warning = 3,
	Unauthorized = 5,
	TimeOut = 6
}

public class ServiceResponse<T> : ServiceResponse
{
	public T Data { get; set; }
}
