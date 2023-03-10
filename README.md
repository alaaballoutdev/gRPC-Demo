# gRPC Demo
This is a demo project for gRPC API implemented in Asp.net Core 7.0 in server and client.

## Table of contents
- [Overview](#overview)
- [How to use it](#how-to-use-it)
- [My process](#my-process)
  - [Built with](#built-with)
  - [What I learned](#what-i-learned)
  - [Useful resources](#useful-resources)
- [Author](#author)


## Overview
Users are able to:

- Add/Update/Delete customer.
- Add/Update/Delete sales of customer.
- View Sales of each customer.
- View all sales of all customers.

It is a basic project to practicing on gRPC architecture.

## How to use it
First of all fill free to clone this repository:

<h3>In GrpcServer:</h3>

- You can choose which database you want to use and add the entity framework core package for your database.

For example in my case:

```
dotnet add package MySql.EntityFrameworkCore 

```

- In `GrpcServer/Models/CustomerDb` edit the code according to your chosen package.

![](./Images/CustomerDb.png)

- In `GrpcServer/appsettings.json` write the connection string of your database.

![](./Images/appsettings.png)

<h3>In GrpcClient:</h3>

- Build the server and run it to figure out the url of your server and use it in `GrpcClient/CustomerClientSingleton`.

- Here we go! Know you can configure projects startup from properties of your solution and run server and client in one click.

## My process

### Built With
- [Asp.net Core 7.0](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)
- [MySql](https://www.mysql.com/) 
- [EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/core/)
- [gRPC](https://learn.microsoft.com/en-us/aspnet/core/grpc?view=aspnetcore-7.0)
- [RazorPages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-7.0&tabs=visual-studio)


### What I Learned
- What is RPC architecture
- Why gRPC
- Where to use it
- And sure how to use it

### Useful Resources
- [What is gRPC](https://youtu.be/gnchfOojMk4)
- [Intro to gRPC in C# - How To Get Started](https://youtu.be/QyxCX2GYHxk)
- [grpc.io](https://grpc.io/)

## Author
- [Alaa Ballout](https://www.linkedin.com/in/alaa-ballout/)
- [alaaballoutdev@gmail.com](mailto:alaaballoutdev@gmail.com)
