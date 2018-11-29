using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nathanielhf_comp2084_assignment_1.Controllers;
using nathanielhf_comp2084_assignment_1.Models;

namespace nathanielhf_comp2084_assignment_1.Tests.Controllers
{
    [TestClass]
    public class DepartmentsControllerTest
    {
        DepartmentsController controller;
        Mock<IDepartmentsMock> mock;
        List<Department> departments;
        Department department;

        [TestInitialize]

        public void TestInitialize()
        {
            // this method reuns automaticall before each individual test

            // new mock data object to hold list of departments
            mock = new Mock<IDepartmentsMock>();

            // populate mock list
            departments = new List<Department>
            {
                new Department
                {
                    department_id = 1, name = "Produce", aisle_number = "1", storage_type = "Misted Coolers"
                },
                new Department
                {
                    department_id = 2, name = "Deli", aisle_number = "2", storage_type = "Bags"
                },
                new Department
                {
                    department_id = 3, name = "Pizza", aisle_number = "3", storage_type = "Freezers"
                }
            };

            // put lis tinto mock object and pass it to the departments controller
            mock.Setup(m => m.Departments).Returns(departments.AsQueryable());
            controller = new DepartmentsController(mock.Object);
        }

        // GET: Departments/Index
        #region
        [TestMethod]
        public void IndexLoadsView()
        {
            // arrange - now moved to TestLinitialize for code re-use
            //DepartmentsController controller = new DepartmentsController();

            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsDepartments()
        {
            // act 
            var result = (List<Department>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(departments, result);
        }
        #endregion

        // GET: Departments/Create
        #region

        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            ViewResult result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        // POST: Departments/Create
        #region
        [TestMethod]
        public void ModelStateNotNullSavesNewRecord()
        {
            // act
            Department copiedDepartmentFromGlobal = department;
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(copiedDepartmentFromGlobal);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion

        // GET: Departments/Delete
        #region
        [TestMethod]
        public void DeleteNoIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(999);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(1);

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsDepartment()
        {
            // act
            // does not include ViewResult, but instead has data that comes with view
            Department result = (Department)((ViewResult)controller.Delete(1)).Model;

            // assert
            Assert.AreEqual(departments[0], result);
        }
        #endregion

        // Post: Departments/DeleteConfirmed
        #region
        [TestMethod]
        public void DeleteConfirmedIdLoadsError()
        {
            //Act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(-1);

            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedNoIdLoadsError()
        {
            //Act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(null);

            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedDataSuccessful()
        {
            //Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion

        // Departments/Details
        #region
        [TestMethod]
        public void DetailsNoIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(534);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsAlbum()
        {
            // act
            Department result = (Department)((ViewResult)controller.Details(1)).Model;

            // assert
            Assert.AreEqual(departments[0], result);
        }
        #endregion

        // GET: Departments/Edit
        #region
        [TestMethod]
        public void EditNoIdLoadsError()
        {
            // arrange
            int? id = null;

            // act
            ViewResult result = (ViewResult)controller.Edit(id);

            // assert 
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditIdIsValidLoadsAlbum()
        {
            // act
            Department result = (Department)((ViewResult)controller.Edit(1)).Model;

            // assert
            Assert.AreEqual(departments[0], result);
        }

        [TestMethod]
        public void EditInvalidIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.Edit(999);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            // act 
            ViewResult result = (ViewResult)controller.Edit(1);

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }
        #endregion

        // POST: Departments/Edit
        #region

        //If Model valid, update album
        [TestMethod]
        public void EditModelIsValidCreateDepartmnet()
        {
            //act 
            Department testDepartment = new Department { department_id = 1 };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(testDepartment);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // If Model valid, return Index view
        [TestMethod]
        public void EditModelIsValidReturnView()
        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(departments[0]);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // If model invalid, return the same view
        [TestMethod]
        public void EditModelInvalidReturnView()
        {
            controller.ModelState.AddModelError("Error", "Error thing");
            Department testDepartment = new Department { department_id = 1 };

            // act
            ViewResult result = (ViewResult)controller.Edit(testDepartment);

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        // If Model invalid, reload the same department
        [TestMethod]
        public void EditModelInvalidReloadDepartment()
        {
            // arrange
            controller.ModelState.AddModelError("Error", "Error thing");
            Department testDepartment = new Department { department_id = 1 };

            // act
            Department result = (Department)((ViewResult)controller.Edit(testDepartment)).Model;

            // assert
            Assert.AreEqual(testDepartment, result);
        }
        #endregion
    }
}
