using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data.Interfaces;

namespace app.Data
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _context;

        private IRoleRepository _roleRepository;
        private IAccountRepository _accountRepository;
        private ISectionRepository _sectionRepository;
        private IForumRepository _forumRepository;
        private ITopicRepository _topicRepository;
        private IPostRepository _postRepository;
        private ITokenRepository _tokenRepository;


        public RepositoryManager(
            RepositoryContext context,
            IRoleRepository role,
            IAccountRepository account,
            ISectionRepository section,
            IForumRepository forum,
            ITopicRepository topic,
            IPostRepository post,
            ITokenRepository token
        )
        {
            _context = context;
            _roleRepository = role;
            _accountRepository = account;
            _sectionRepository = section;
            _forumRepository = forum;
            _topicRepository = topic;
            _postRepository = post;
            _tokenRepository = token;
        }

        public IRoleRepository Role => _roleRepository;
        public IAccountRepository Account => _accountRepository;
        public ISectionRepository Section => _sectionRepository;
        public IForumRepository Forum => _forumRepository;
        public ITopicRepository Topic => _topicRepository;
        public IPostRepository Post => _postRepository;
        public ITokenRepository Token => _tokenRepository;
            

        public void BeginTransaction() =>
            _context.Database.BeginTransaction();

        public async Task BeginTransactionAsync() =>
            await _context.Database.BeginTransactionAsync();
        
        public void Commit() =>
            _context.Database.CommitTransaction();

        public async Task CommitAsync() =>
            await _context.Database.CommitTransactionAsync();

        public void Rollback() =>
            _context.Database.RollbackTransaction();

        public async Task RollbackAsync() =>
            await _context.Database.RollbackTransactionAsync();

        public void Save() =>
            _context.SaveChanges();

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}