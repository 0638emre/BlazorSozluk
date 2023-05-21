namespace BlazorSozluk.Common.Models
{
    public enum VoteTypes
    {
        /// <summary>
        /// oy yok ise
        /// </summary>
        None = -1,

        /// <summary>
        /// oy olumsuz ise
        /// </summary>
        DownVote = 0,

        /// <summary>
        /// oy olumlu ise
        /// </summary>
        UpVote = 1
    }
}
