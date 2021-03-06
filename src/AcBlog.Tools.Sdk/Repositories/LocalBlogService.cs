﻿using AcBlog.Data.Models;
using AcBlog.Data.Models.Actions;
using AcBlog.Data.Repositories;
using AcBlog.Sdk;
using AcBlog.Services;
using AcBlog.Services.Models;
using AcBlog.Tools.Sdk.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AcBlog.Tools.Sdk.Repositories
{
    public class LocalBlogService : IBlogService
    {
        public LocalBlogService(string rootPath)
        {
            RootPath = rootPath;
            InnerPostService = new PostService(this, Path.Join(rootPath, "posts"));
            PageService = new PageService(this, Path.Join(rootPath, "pages"));
            LayoutService = new LayoutService(this, Path.Join(rootPath, "layouts"));
        }

        public string RootPath { get; }

        internal PostService InnerPostService { get; }

        public IPostService PostService => InnerPostService;

        public IPageService PageService { get; }

        public ILayoutService LayoutService { get; }

        public ICommentService CommentService => throw new System.NotImplementedException();

        public IStatisticService StatisticService => throw new System.NotImplementedException();

        public IFileService FileService => throw new System.NotImplementedException();

        public IUserService UserService => throw new NotImplementedException();

        public RepositoryAccessContext Context { get; set; } = new RepositoryAccessContext();

        public async Task<BlogOptions> GetOptions(CancellationToken cancellationToken = default)
        {
            string path = Path.Join(RootPath, Workspace.BlogOptionPath);
            if (System.IO.File.Exists(path))
            {
                await using var st = System.IO.File.OpenRead(path);
                return
                    await JsonSerializer.DeserializeAsync<BlogOptions>(st, cancellationToken: cancellationToken).ConfigureAwait(false)
                    ?? throw new NullReferenceException("Options is null");
            }
            else
            {
                return new BlogOptions();
            }
        }

        public Task<bool> SetOptions(BlogOptions options, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<QueryResponse<string>> Query(BlogQueryRequest query, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
