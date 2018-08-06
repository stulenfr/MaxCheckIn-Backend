﻿using ClassSignIn_Hackathon_BE.Models;
using ClassSignIn_Hackathon_BE.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassSignIn_Hackathon_BE.Controllers
{
    public class StudentsController : ApiController { 
		private LogDbContext db = new LogDbContext();

			////Authenticate user
			//[HttpGet]
			//[ActionName("Authenticate")]
			//public JsonResponse Authenticate(string username, string password) {
			//	if(username == null || password == null) {
			//		return new JsonResponse { Result = "Failed", Message = "Authentication Failed: No Username or Password" };
			//	}
			//	var user = db.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
			//	if(user == null) {
			//		return new JsonResponse { Result = "Failed", Message = "Authentication Failed: Incorrect Username/Password" };
			//	}
			//	return new JsonResponse { Data = user };
			//}


			//GET-ALL
			//indicates that a get method will be used to get this info vs. post which updates
			[HttpGet]
			[ActionName("List")] //this is the name the client will use to call this method
			public JsonResponse List() {
				return new JsonResponse {
					Data = db.Students.ToList()
				};
			}

			//GET-ONE
			[HttpGet]
			[ActionName("Get")]
			public JsonResponse Student(int? id) {
				if(id == null) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Id does not exist"
					};
				}
				return new JsonResponse {
					Data = db.Students.Find(id)
				};
			}

			//POST
			[HttpPost]
			[ActionName("Create")]
			public JsonResponse Create(Student student) {
				if(student == null) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Create requires an instance of Student"
					};
				}
				if(!ModelState.IsValid) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Model state is invalid. See data.",
						Error = ModelState
					};
				}

				db.Students.Add(student);
				db.SaveChanges();
				return new JsonResponse {
					Message = "Create successful.",
					Data = student
				};
			}

			//CHANGE
			[HttpPost]
			[ActionName("Change")]
			public JsonResponse Change(Student student) {
				if(student == null) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Create requires an instance of Student"
					};
				}
				if(!ModelState.IsValid) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Model state is invalid. See data.",
						Error = ModelState
					};
				}
				db.Entry(student).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				return new JsonResponse {
					Message = "Change successful.",
					Data = student
				};
			}

			//DELETE
			[HttpPost]
			[ActionName("Remove")]
			public JsonResponse Remove(Student student) {
				if(student == null) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Create requires an instance of Student"
					};
				}
				if(!ModelState.IsValid) {
					return new JsonResponse {
						Result = "Failed",
						Message = "Model state is invalid. See data.",
						Error = ModelState
					};
				}
				db.Entry(student).State = System.Data.Entity.EntityState.Deleted;
				db.SaveChanges();
				return new JsonResponse {
					Message = "Remove successful.",
					Data = student
				};
			}

			//REMOVE/ID
			[HttpPost]
			[ActionName("RemoveId")]
			public JsonResponse Remove(int? id) {
				if(id == null)
					return new JsonResponse {
						Result = "Failed",
						Message = "RemoveId requires a Student.Id"
					};
				var student = db.Students.Find(id);
				if(student == null)
					return new JsonResponse {
						Result = "Failed",
						Message = $"No Student have Id of {id}"
					};
				db.Students.Remove(student);
				db.SaveChanges();
				return new JsonResponse {
					Message = "Remove successful.",
					Data = student
				};
			}

		}
}


