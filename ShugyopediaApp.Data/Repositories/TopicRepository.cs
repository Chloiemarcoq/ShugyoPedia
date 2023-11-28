using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
	public class TopicRepository : BaseRepository, ITopicRepository
	{
		public TopicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{

		}
		public IQueryable<Topic> GetTopics()
		{
			return this.GetDbSet<Topic>();
		}
	}
}
