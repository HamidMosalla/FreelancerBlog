# WebFor AspNetCore
I've started building this project when Asp.Net Core RC1 was released, and I've added new features to it and upgrade it ever since. At first my intention was to fiddle with Asp.Net Core a little to learn, but then I've decided to make it into a project that can be useable. This project hasn't built to be a flexible CMS, but that doesn't mean you can't use bits and pieces of it in your project, because many parts of this project are needs that reoccur. If you saw some gobbledygook characters don't panic, it's because the main language of this blog was Persian, but I'm in the process of transforming it to support multiple language using Asp.Net Core built in localization mechanism. 

#What's in it?  

## Server Side:
C#  
Asp.Net Core 1.0 RTM  
Entity Framework Core 1.0 RTM  
NETStandard.Library 1.6  
AutoMapper 5.0.2  
Autofac 4.0.0-rc3-293  
x-Unit 2.2.0-beta2-build3300  
Moq 4.6.25-alpha  
GenFu 1.1.1  
FluentAssertions 4.13.0  

##Client Side:
JavaScript  
jQuery  
Bootstrap  
CkEditor  
Font Awesome  
jQuery blockUI  
Modernizr  
Requirejs  
Spin.js  
RespondJs  

##Practices and Paradigm:
SOLID  
Ports and Adapters Architecture    
DDD  
Dependency Inversion Principle  
Repository Pattern  
Unit Of Work Pattern  
Adapter Pattern  
Strategy Pattern  
JavaScript Module Pattern  
Unit Test  

As you can see a lot of this server side technologies are in beta (except the Asp.Net Core itself), but I continue to update them as newer versions become available

#What you need to run it?
Visual Studio 2015 Update 3  
SQL Server  

#Things to note
Please note that the use of some paradigms in this project is an overkill, you most certainly don't need a multi layered onion architecture for a simple blog, one should introduce complexity when there is a need for it as [YAGNI](http://deviq.com/yagni/) principle states, but I've done it mostly for educational purposes and also I wanted to have a good jump start template for more complex projects.
