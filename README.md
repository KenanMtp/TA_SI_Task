# [PRO] Standalone executable Selenium test suite
# [PRO] Standalone thinking-tester-contact-list.herokuapp executable Selenium test case
SI_ContactList with profile *standalone* is meant to be used for building standalone executable Selenium test suite, used by QA engineers to succesfully execute these tests.

SI_ContactList tests:
- AddContactToContactList

## Usage
1. Check if using Visual studio Comunity edition
2. Install Microsoft Visual Studio Community 2022 (64-bit)
3. Install .NET 8.x version 
4. Install Chrome browser
5. Donwload ChromeDriver (version compatible to Chrome browser version that is installed)
6. Install Chrome browser
7. Right-click on the Project in Solution Explorer and Select Manage Nuget Packages
8. In the Nuget Package Manager Window > Browse, enter Selenium and install the package
    a) Selenium.Web.Driver
    b) Selenium.Support
9. Under SI_ContactList project create "drivers" directory
10. Right-click on "drivers" directory, Add -> Existing Item... , open folder where is chrome driver located
    and select chromedriver.exe (same procedure is for any other driver/browser)
11.  Run the test using Test Explorer, click on Test -> TestExplorer, and then select test("AddContactToContactList")
     to run and click on Run
