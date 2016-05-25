ProjectTemplate
================
A template for C# projects. This will help so that all our projects are started the same way, using the same tools.

## Usage
1. Clone this repository
2. Change the name of the "ProjectTemplate" folder to the name of your new project.
3. Change the name of the .sln and .sln.DotSettings file to the name of your new project.
4. Open the solution, and then the properties of the "View" project. Change the "Assembly name" to the name of your new project.
5. Change the "Title" resource of the MainWindowView.resx file to the name of your project.
6. Change "README.md" so that the name of the project is used, and all of this information is removed.
7. Commit changes to your local git repository.
8. Create a new repository on github.com (or any other offsite repository) for your project.
9. Under git settings change the URL of your "origin" remote to the address of the new offsite repository.
10. Push to the new repository.

## Visual Studio Version
### 2012
This template was originally created using VS 2012.
### 2013
The template opened up without any problems in VS 2013.

## ReSharper Version
### 7.0
This template was originally created using ReSharper 7.0.
### 9.0
At version 0.0.0.21 the .sln.DotSettings file was upgraded to support ReSharper 9.0.

## Version Number
As seen in "Properties/AssemblyInfo.cs" file of the "View" project the version number is constructed as "major.minor.build.revision".

The "major.minor.build" is set by adding a tag to a commit on the git repository (ie 0.1.0). The "revision" will indicate how many commits since the tagged commit.

The easiest way to get run-time access to the version number (and other assembly values) is through the "AssemblySettings" property of the "MainWindowViewModel".

## Logging
A daily log file is generated, and can be written to via a Logger property that can be created in any class:
```c#
private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
```

## Debug vs Release
When an unhandled exception is encountered the two builds will behave differently. 
* Debug - halt on an unhandled exception
* Release - Open a dialogue that contains information about the exception and allows the user to send an email to the developers

## Localization
To add a localized language see the file "View\LocalizationHelper.bat". By default resource dlls are only created as a post-build step of "Release" builds (to save time).

To make it easier to use localized resources in xaml files a 3rd party tool is used: [Resx](http://www.codeproject.com/Articles/35159/WPF-Localization-Using-RESX-Files).

The "CultureSettings" property of the "MainWindowViewModel" can be used at runtime to get the list of supported languages, and to change the used language.

## Dialogs
To show dialogs in an MVVM manner the MvvmDialogs [library](http://www.codeproject.com/Articles/820324/Implementing-Dialog-Boxes-in-MVVM) is used. This library should be used for to wrap .net and 3rd party dialogs.

Custom dialogs should be implemented using the "Caliburn.Micro" style. See the "ExceptionView" and "ExceptionViewModel" classes for an example of this.

Either kind can be displayed using the "DialogServices" class. Any view model that needs to display a dialog should use Ninject to inject an instance of an object that implements the "IDialogServices" interface.

## Custom Dictionary
At times we will want to use words in the code (either as code, or as comments) that are not recognized as correctly spelled words. To correct this they must be added to the CustomDictionary.xml file in the root directory.

To make sure the CustomDictionary.xml file is used without being interferred with make sure there isn't a file named CustomDictionary.xml in this system folder: "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Team Tools\Static Analysis Tools\FxCop\

## Ninject
The DI framework Ninject is used, and whenever possible dependencies should be passed in on the constructor and Ninject should be allowed to create them. This will facilitate unit testing as you will only be testing the class in question and not the dependent classes. See "MainWindowViewModel.cs" for an example.

## Running Unit Tests
Unit tests can be run/debugged through Visual Studio or Resharper. To run all the tests through Resharper go to "Resharper" > "Unit Tests" > "Run All Tests from Solution".

100% code coverage through unit tests is a praisworthy goal. To test code coverage go to "Test" > "Analyze Code Coverage" > "All Tests".

Unit tests have been written such that upon downloading there is 100% code coverage.

### Mocking
A mocking framework (Moq) is used to facilitate unit testing. This means that if you have used dependency injection throughout development you can mock the dependencies and have them act in any way you desire. This makes it easy to test scenarios and be sure that you are testing just the class in question, and not any dependencies. See "MainWindowViewModelTest.cs" for examples.

## The Cops!
* StyleCop - is used to enforce consistent code style standards. Violations will be output in the "Error List" as warnings, and can often be viewed on the right hand scroll bar through Resharper.
* FxCop - is a code analysis tool built into Visual Studio. This tools helps promote best practices. Violations will be output in the "Error List" as warnings.

Where possible code should not be committed that contains warnings whether they be from the compiler, StyleCop, or FxCop.

##To Do
Add deployment functionality to the template.
