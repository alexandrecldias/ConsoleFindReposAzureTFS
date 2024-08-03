using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFindObjetosNoTFS
{
    public class SearchResponse
    {
        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("filterCategories")]
        public List<FilterCategory> FilterCategories { get; set; }
    }

    public class Query
    {
        [JsonProperty("searchText")]
        public string SearchText { get; set; }

        [JsonProperty("skipResults")]
        public int SkipResults { get; set; }

        [JsonProperty("takeResults")]
        public int TakeResults { get; set; }

        [JsonProperty("filters")]
        public List<object> Filters { get; set; }

        [JsonProperty("searchFilters")]
        public SearchFilters SearchFilters { get; set; }

        [JsonProperty("sortOptions")]
        public List<object> SortOptions { get; set; }

        [JsonProperty("summarizedHitCountsNeeded")]
        public bool SummarizedHitCountsNeeded { get; set; }
    }

    public class SearchFilters
    {
    }

    public class Results
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("values")]
        public List<ResultValue> Values { get; set; }
    }

    public class ResultValue
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("hitCount")]
        public int HitCount { get; set; }

        [JsonProperty("hits")]
        public List<Hit> Hits { get; set; }

        [JsonProperty("matches")]
        public Matches Matches { get; set; }

        [JsonProperty("collection")]
        public string Collection { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("repository")]
        public string Repository { get; set; }

        [JsonProperty("repositoryId")]
        public string RepositoryId { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("versions")]
        public List<Version> Versions { get; set; }

        [JsonProperty("changeId")]
        public string ChangeId { get; set; }

        [JsonProperty("contentId")]
        public string ContentId { get; set; }

        [JsonProperty("vcType")]
        public string VcType { get; set; }
    }

    public class Hit
    {
        [JsonProperty("charOffset")]
        public int CharOffset { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }

    public class Matches
    {
        [JsonProperty("content")]
        public List<ContentMatch> Content { get; set; }

        [JsonProperty("fileName")]
        public List<object> FileName { get; set; }
    }

    public class ContentMatch
    {
        [JsonProperty("charOffset")]
        public int CharOffset { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }

    public class Version
    {
        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        [JsonProperty("changeId")]
        public string ChangeId { get; set; }
    }

    public class FilterCategory
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }
    }

    public class Filter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resultCount")]
        public int ResultCount { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}
