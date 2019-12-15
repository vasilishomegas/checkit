# ListIt

The solution contains 5 projects:
 - ListIt_WebFrontend: The user interface.
 - ListIt_WindowsBackend: The management program for admins.
 - ListIt_DomainModel: The models that will be used through the whole solution.
 - ListIt_DataAccessLayer: This will take care of access to the database and contain LINQ queries.
 - ListIt_DataAccessModel: DB Models which are used by DAL, and by Service and Controllers in order to generate generic classes and avoid connecting Controllers directly to DAL.
 - ListIt_BusinessLogic: This will link everything together, and will provide the user interfaces with data from the DataAccessLayer in the format of the DomainModel.
 - ListIt_WebAPI: The layer for RESTful service of the project - neccessery to connect our project with everything what is not based on .NET platform (i.e. phone app if we made one)