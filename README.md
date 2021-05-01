# Covid19 Observer

### About
This application displays Covid 19 data from countries all over the world. Data that are available are the following:
* Date of Observation
* Country
* Number of Confirmed Cases
* Number of Deaths
* Number of Recovered Patients

#### Data source
Data are loaded from a csv file.

#### Project structure
The Solution file has two Projects: API and Web.
* API project houses the Controllers, Services, DB context.
* Web project consumes the API

### Limitations
This application does not allow user to import CSV files, nor link to a online CSV file.

### Software
* PostgreSQL v13.x and pgdmin 4 (installed along with PostgreSQL 13)
* .Net 5 SDK
* node v14.5.0
* Visual Studio 2019 v16.9
* Visual Studio Code 1.55.2

### Installation
In this guide, you'll need a **CLI**. You can either use VS 2019's **Package Manager Console**, **VS Code's terminal**, or the classic **Windows CMD**, etc. I suggest you open two (2) CLIs, one for **node**, one for **dotnet**. 

Scripts run in **node** will be pointed at the **Covid19Observer.Web** project, while scripts run in **dotnet** will be pointed at **Covid19Observer.API** project. For us not to fiddle with directories during the guide, setup each CLIs now by running:

If your CLI is already open in the **Solution level** (<repo directory>/COVID19Observer)
run ``cd Covid19Observer.Web``
if not, run `cd <repo directory>/COVID19Observer/Covid19Observer.Web`

**Do the same with the other CLI, pointing to Covid19Observer.API**

Throughout the guide, I'll pertain to the CLI pointing to Covid19Observer.Web as **Web CLI**, and Covid19Observer.API as **API CLI**
#### Project
1. Clone this project
2. Open the solution with **Visual Studio 2019**. The solution should be in the project's root folder named **COVID19Observer.sln**. The NuGet packages should be restored.
    
    If they're not, you can manually restore them yourself: In the **Solution Explorer**, right click on the **COVID19Observer solution**, and hit **Restore NuGet Packages**
3. In your **Web CLI** : Run ``npm i``
 
#### Initialize Database
1. In your **API CLI** run `dotnet ef database update`

### Running the Application
1. In your **Visual Studio 2019**, set the API project as Startup project
    1. Right click on the **Covid19Observer.API** Project
    2. Click on the **Set as startup Project**
2. Run the API by clicking on the **Run button labelled IIS EXPRESS**
3. In your **Web CLI** run ``npm run serve``

