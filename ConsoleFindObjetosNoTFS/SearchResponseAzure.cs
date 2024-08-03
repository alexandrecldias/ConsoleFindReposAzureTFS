using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFindObjetosNoTFS
{
    public class SearchResponseAzure
    {
        public int count { get; set; }
        public Result[] results { get; set; }
        public int infoCode { get; set; }
        public Facets facets { get; set; }

    }

        public class Facets
        {
            public Project[] Project { get; set; }
        }

        public class Project
        {
            public string name { get; set; }
            public string id { get; set; }
            public int resultCount { get; set; }
        }

        public class Result
        {
            public string fileName { get; set; }
            public string path { get; set; }
            public MatchesAzure matches { get; set; }
            public Collection collection { get; set; }
            public Project1 project { get; set; }
            public Repository repository { get; set; }
            public VersionAzure[] versions { get; set; }
            public string contentId { get; set; }
        }

        public class MatchesAzure
        {
            public Content[] content { get; set; }
            public object[] fileName { get; set; }
        }

        public class Content
        {
            public int charOffset { get; set; }
            public int length { get; set; }
            public int line { get; set; }
            public int column { get; set; }
            public object codeSnippet { get; set; }
            public string type { get; set; }
        }

        public class Collection
        {
            public string name { get; set; }
        }

        public class Project1
        {
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Repository
        {
            public string name { get; set; }
            public string id { get; set; }
            public string type { get; set; }
        }

        public class VersionAzure
        {
            public string branchName { get; set; }
            public string changeId { get; set; }
        }

    }

