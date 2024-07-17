I have created the skeleton framework and automated all API.
xUnit is a perfect runner for unit tests and part of integration tests, but if considering other areas, especially Web testing, xUnit has a lot of drawbacks (on one of project we were forced to use xUnit but after a lot of different limitations and issues we redesigned it to nUnit).
There are 3 basic layers - for framework itself, for API clients and for tests.
Actually, there are three main approaches to such an automation:
1) using analogues from page objects (e.g. API objects, or clients) - used on most projects by TA engineer, so I picked up it here
2) using request builders, omitting API clients logic (so there can be a framework and test layer, and info about resources, parameters and other things are used in test layer), sometimes even combining with SpecFlow (which is adding an extra layer of maintenance).
3) using auto-generation of clients based on Swagger documentation (which we have), and writing only tests (it has significant advantage of saving most of time for covering API, but it has major drawbacks in scope of stability and maintenance, and most of the projects does not considering it, unless logic is very simple and API tests needs to be written ASAP)
So I choose 1st approach.

I have spent nearly 3 hours on this task, but there are a lot of things that might be refactored or improved (generally good framework and architecture creation might take weeks). E.g. fakers can be extracted to another class that will omit duplications. Or redesign a little structure for TestCaseSource, as well as other things.

The parallel execution is also turned on. it can be configured in a base test (or adjusted to be able to configure from TestConfiguration).

Also it has logging and Allure report support (e.g. for more details specified tags, like AllureOwner, etc can be added).

Asserts are build in a way so if first is failed - all other will be checked as well, and final assertion message will be generated. As well as giving full representation of request and response on fail.