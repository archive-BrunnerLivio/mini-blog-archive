﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.ViewModels;

namespace M183_Blog.Controllers
{
    /**
     * 1 )
     * Er ist einfach anzuwenden, jedoch relativ sicher dank der
     * iterationszahl & salt kombination. Das diese Kombination wird
     * vor nicht-quanten-computer relativ sicher sein, da die Zeit
     * um dies zu brute-forcen zu lange dauern würde. Auch wäre eine
     * Rainbowtable nutzlos.
     * 
     * 2) 
     * Man kann den User vor ungenehmtige Logins schützen, indem man
     * ein Mail sendet, jedes mal wenn sich ein User mit einer anderen IP
     * einloggt.
     * 
     * 
     * */
    public class HomeController : Controller
    {
        public DataContext db = new DataContext();

        private PostRepository postRepository;

        // GET: Home
        public ActionResult Index()
        {
            postRepository = new PostRepository(db);
            List<Post> posts = postRepository.GetPublicPosts().ToList();
            return View(new HomeViewModel(posts));
        }

        public ActionResult Seed()
        {
            Claim claim1 = new Claim
            {
                Name = Claims.Admin
            };
            Claim claim2 = new Claim
            {
                Name = Claims.Comment
            };
            Claim claim3 = new Claim
            {
                Name = Claims.Create
            };
            User user1 = new User
            {
                Username = "yvesloy",
                Password = PasswordHash.Hash("admin"),
                Firstname = "Yves",
                Familyname = "Zumbühl",
                Mobilephonenumber = "0041799506553", //ToDo: Claims sind immer Null, behebe
                Claims = new List<Claim> { claim1 },
                Status = " retro poke try-hard brooklyn"
            };

            User user2 = new User()
            {
                Username = "BrunnerLivio",
                Password = PasswordHash.Hash("1234"),
                Firstname = "Livio",
                Familyname = "Brunner",
                Mobilephonenumber = "0041793189773",
                Claims = new List<Claim> { claim2, claim3 },
                Status = "Heirloom XOXO pitchfork, polaroid cliche" 
            };

            Comment comment1 = new Comment
            {
                Commet = "Awesome!",
                CreatedOn = DateTime.Today.AddDays(-1),
                User = user1
            };
            Comment comment2 = new Comment
            {
                Commet = "Thanks for this article!",
                CreatedOn = DateTime.Today.AddDays(-2),
                User = user2
            };
            Comment comment3 = new Comment
            {
                Commet = "cool",
                CreatedOn = DateTime.Today,
                User = user1
            };
            db.Posts.AddRange(new List<Post>()
            {
                new Post()
                {
                    Content =
                        "Austin williamsburg vice salvia. Fashion axe occupy selvage vegan. " +
                        "DIY meditation tacos quinoa ennui, hell of intelligentsia health goth " +
                        "mustache drinking vinegar gluten-free butcher. Tofu hammock gluten-free adaptogen.",
                    CreatedOn = DateTime.Now.AddHours(-100),
                    Description = "Lorem ipsum dolor amet retro",
                    Status = PostStatus.Public,
                    Title = "Hipster Ipsum",
                    User = user1,
                    Comment = new List<Comment>() {comment3, comment2}
                },
                new Post()
                {
                    Content = "Heirloom letterpress meggings wayfarers, +1 hexagon fixie mumblecore" +
                              " kinfolk stumptown you probably haven't heard of them next level paleo " +
                              "cornhole swag. Pok pok before they sold out thundercats, air plant kickstarter " +
                              "crucifix keffiyeh occupy gastropub echo park meh af marfa. Selvage vinyl " +
                              "helvetica leggings la croix waistcoat kickstarter four dollar toast post-ironic. " +
                              "Tofu literally pitchfork, mumblecore food truck distillery messenger bag cronut sustainable.",
                    CreatedOn = DateTime.Now.AddHours(-200),
                    Description = "iPhone echo park knausgaard williamsburg",
                    Status = PostStatus.Public,
                    Title = "This Post is already public",
                    User = user2,
                    Comment = new List<Comment>() {comment1, comment2}
                },
                new Post()
                {
                    Content = "Heirloom letterpress meggings wayfarers, +1 hexagon fixie mumblecore" +
                              " kinfolk stumptown you probably haven't heard of them next level paleo " +
                              "cornhole swag. Pok pok before they sold out thundercats, air plant kickstarter " +
                              "crucifix keffiyeh occupy gastropub echo park meh af marfa. Selvage vinyl " +
                              "helvetica leggings la croix waistcoat kickstarter four dollar toast post-ironic. " +
                              "Tofu literally pitchfork, mumblecore food truck distillery messenger bag cronut sustainable.",
                    CreatedOn = DateTime.Now.AddHours(-200),
                    Description = "The Moment",
                    Status = PostStatus.Private,
                    Title = "This Post is still private",
                    User = user2,
                    Comment = new List<Comment>() {comment1, comment2}
                },
                new Post()
                {
                    Content = "Heirloom letterpress meggings wayfarers, +1 hexagon fixie mumblecore" +
                              " kinfolk stumptown you probably haven't heard of them next level paleo " +
                              "cornhole swag. Pok pok before they sold out thundercats, air plant kickstarter " +
                              "crucifix keffiyeh occupy gastropub echo park meh af marfa. Selvage vinyl " +
                              "helvetica leggings la croix waistcoat kickstarter four dollar toast post-ironic. " +
                              "Tofu literally pitchfork, mumblecore food truck distillery messenger bag cronut sustainable.",
                    CreatedOn = DateTime.Now.AddHours(-200),
                    Description = "iPhone echo park knausgaard williamsburg",
                    Status = PostStatus.Private,
                    Title = "This Post is still private",
                    User = user2,
                    Comment = new List<Comment>() {comment1, comment2}
                }
            });
            db.SaveChanges();
            return new HttpStatusCodeResult(200);
        }
    }
}