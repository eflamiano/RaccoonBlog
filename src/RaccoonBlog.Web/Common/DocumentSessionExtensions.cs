using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using RaccoonBlog.Web.Infrastructure.AutoMapper;
using RaccoonBlog.Web.Models;
using RaccoonBlog.Web.ViewModels;
using Raven.Client;
using Raven.Client.Linq;

namespace RaccoonBlog.Web.Common
{
	public static class DocumentSessionExtensions
	{
		public static PostReference GetPostReference(this IDocumentSession session, Expression<Func<Post, bool>> expression, bool desc)
		{
			var queryable = session.Query<Post>()
				.WhereIsPublicPost()
				.Where(expression);

			queryable = desc ? 
				queryable.OrderByDescending(post => post.PublishAt) : 
				queryable.OrderBy(post => post.PublishAt);

			var postReference = queryable
			  
			  .Select(p => new PostReference{ Id = p.Id, Title = p.Title })
			  .FirstOrDefault();

			if (postReference == null)
				return null;

			return postReference;
		}

		public static User GetCurrentUser(this IDocumentSession session)
		{
			if (HttpContext.Current.Request.IsAuthenticated == false)
				return null;

			var email = HttpContext.Current.User.Identity.Name;
			var user = session.GetUserByEmail(email);
			return user;
		}

		public static User GetUserByEmail(this IDocumentSession session, string email)
		{
			return session.Query<User>()
				.Where(u => u.Email == email)
				.FirstOrDefault();
		}
	}
}
