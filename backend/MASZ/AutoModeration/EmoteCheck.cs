using Discord;
using MASZ.Models;
using System.Text.RegularExpressions;

namespace MASZ.AutoModeration
{
    public static class EmoteCheck
    {
        private static readonly Regex _emoteRegex = new(@"(👨🏿‍❤️‍💋‍👨🏿|👨🏿‍❤️‍💋‍👨🏻|👨🏿‍❤️‍💋‍👨🏾|👨🏿‍❤️‍💋‍👨🏼|👨🏿‍❤️‍💋‍👨🏽|👨🏻‍❤️‍💋‍👨🏻|👨🏻‍❤️‍💋‍👨🏿|👨🏻‍❤️‍💋‍👨🏾|👨🏻‍❤️‍💋‍👨🏼|👨🏻‍❤️‍💋‍👨🏽|👨🏾‍❤️‍💋‍👨🏾|👨🏾‍❤️‍💋‍👨🏿|👨🏾‍❤️‍💋‍👨🏻|👨🏾‍❤️‍💋‍👨🏼|👨🏾‍❤️‍💋‍👨🏽|👨🏼‍❤️‍💋‍👨🏼|👨🏼‍❤️‍💋‍👨🏿|👨🏼‍❤️‍💋‍👨🏻|👨🏼‍❤️‍💋‍👨🏾|👨🏼‍❤️‍💋‍👨🏽|👨🏽‍❤️‍💋‍👨🏽|👨🏽‍❤️‍💋‍👨🏿|👨🏽‍❤️‍💋‍👨🏻|👨🏽‍❤️‍💋‍👨🏾|👨🏽‍❤️‍💋‍👨🏼|🧑🏿‍❤️‍💋‍🧑🏻|🧑🏿‍❤️‍💋‍🧑🏾|🧑🏿‍❤️‍💋‍🧑🏼|🧑🏿‍❤️‍💋‍🧑🏽|🧑🏻‍❤️‍💋‍🧑🏿|🧑🏻‍❤️‍💋‍🧑🏾|🧑🏻‍❤️‍💋‍🧑🏼|🧑🏻‍❤️‍💋‍🧑🏽|🧑🏾‍❤️‍💋‍🧑🏿|🧑🏾‍❤️‍💋‍🧑🏻|🧑🏾‍❤️‍💋‍🧑🏼|🧑🏾‍❤️‍💋‍🧑🏽|🧑🏼‍❤️‍💋‍🧑🏿|🧑🏼‍❤️‍💋‍🧑🏻|🧑🏼‍❤️‍💋‍🧑🏾|🧑🏼‍❤️‍💋‍🧑🏽|🧑🏽‍❤️‍💋‍🧑🏿|🧑🏽‍❤️‍💋‍🧑🏻|🧑🏽‍❤️‍💋‍🧑🏾|🧑🏽‍❤️‍💋‍🧑🏼|👩🏿‍❤️‍💋‍👨🏿|👩🏿‍❤️‍💋‍👨🏻|👩🏿‍❤️‍💋‍👨🏾|👩🏿‍❤️‍💋‍👨🏼|👩🏿‍❤️‍💋‍👨🏽|👩🏻‍❤️‍💋‍👨🏻|👩🏻‍❤️‍💋‍👨🏿|👩🏻‍❤️‍💋‍👨🏾|👩🏻‍❤️‍💋‍👨🏼|👩🏻‍❤️‍💋‍👨🏽|👩🏾‍❤️‍💋‍👨🏾|👩🏾‍❤️‍💋‍👨🏿|👩🏾‍❤️‍💋‍👨🏻|👩🏾‍❤️‍💋‍👨🏼|👩🏾‍❤️‍💋‍👨🏽|👩🏼‍❤️‍💋‍👨🏼|👩🏼‍❤️‍💋‍👨🏿|👩🏼‍❤️‍💋‍👨🏻|👩🏼‍❤️‍💋‍👨🏾|👩🏼‍❤️‍💋‍👨🏽|👩🏽‍❤️‍💋‍👨🏽|👩🏽‍❤️‍💋‍👨🏿|👩🏽‍❤️‍💋‍👨🏻|👩🏽‍❤️‍💋‍👨🏾|👩🏽‍❤️‍💋‍👨🏼|👩🏿‍❤️‍💋‍👩🏿|👩🏿‍❤️‍💋‍👩🏻|👩🏿‍❤️‍💋‍👩🏾|👩🏿‍❤️‍💋‍👩🏼|👩🏿‍❤️‍💋‍👩🏽|👩🏻‍❤️‍💋‍👩🏻|👩🏻‍❤️‍💋‍👩🏿|👩🏻‍❤️‍💋‍👩🏾|👩🏻‍❤️‍💋‍👩🏼|👩🏻‍❤️‍💋‍👩🏽|👩🏾‍❤️‍💋‍👩🏾|👩🏾‍❤️‍💋‍👩🏿|👩🏾‍❤️‍💋‍👩🏻|👩🏾‍❤️‍💋‍👩🏼|👩🏾‍❤️‍💋‍👩🏽|👩🏼‍❤️‍💋‍👩🏼|👩🏼‍❤️‍💋‍👩🏿|👩🏼‍❤️‍💋‍👩🏻|👩🏼‍❤️‍💋‍👩🏾|👩🏼‍❤️‍💋‍👩🏽|👩🏽‍❤️‍💋‍👩🏽|👩🏽‍❤️‍💋‍👩🏿|👩🏽‍❤️‍💋‍👩🏻|👩🏽‍❤️‍💋‍👩🏾|👩🏽‍❤️‍💋‍👩🏼|👨🏿‍❤‍💋‍👨🏿|👨🏿‍❤‍💋‍👨🏻|👨🏿‍❤‍💋‍👨🏾|👨🏿‍❤‍💋‍👨🏼|👨🏿‍❤‍💋‍👨🏽|👨🏻‍❤‍💋‍👨🏻|👨🏻‍❤‍💋‍👨🏿|👨🏻‍❤‍💋‍👨🏾|👨🏻‍❤‍💋‍👨🏼|👨🏻‍❤‍💋‍👨🏽|👨🏾‍❤‍💋‍👨🏾|👨🏾‍❤‍💋‍👨🏿|👨🏾‍❤‍💋‍👨🏻|👨🏾‍❤‍💋‍👨🏼|👨🏾‍❤‍💋‍👨🏽|👨🏼‍❤‍💋‍👨🏼|👨🏼‍❤‍💋‍👨🏿|👨🏼‍❤‍💋‍👨🏻|👨🏼‍❤‍💋‍👨🏾|👨🏼‍❤‍💋‍👨🏽|👨🏽‍❤‍💋‍👨🏽|👨🏽‍❤‍💋‍👨🏿|👨🏽‍❤‍💋‍👨🏻|👨🏽‍❤‍💋‍👨🏾|👨🏽‍❤‍💋‍👨🏼|🧑🏿‍❤‍💋‍🧑🏻|🧑🏿‍❤‍💋‍🧑🏾|🧑🏿‍❤‍💋‍🧑🏼|🧑🏿‍❤‍💋‍🧑🏽|🧑🏻‍❤‍💋‍🧑🏿|🧑🏻‍❤‍💋‍🧑🏾|🧑🏻‍❤‍💋‍🧑🏼|🧑🏻‍❤‍💋‍🧑🏽|🧑🏾‍❤‍💋‍🧑🏿|🧑🏾‍❤‍💋‍🧑🏻|🧑🏾‍❤‍💋‍🧑🏼|🧑🏾‍❤‍💋‍🧑🏽|🧑🏼‍❤‍💋‍🧑🏿|🧑🏼‍❤‍💋‍🧑🏻|🧑🏼‍❤‍💋‍🧑🏾|🧑🏼‍❤‍💋‍🧑🏽|🧑🏽‍❤‍💋‍🧑🏿|🧑🏽‍❤‍💋‍🧑🏻|🧑🏽‍❤‍💋‍🧑🏾|🧑🏽‍❤‍💋‍🧑🏼|👩🏿‍❤‍💋‍👨🏿|👩🏿‍❤‍💋‍👨🏻|👩🏿‍❤‍💋‍👨🏾|👩🏿‍❤‍💋‍👨🏼|👩🏿‍❤‍💋‍👨🏽|👩🏻‍❤‍💋‍👨🏻|👩🏻‍❤‍💋‍👨🏿|👩🏻‍❤‍💋‍👨🏾|👩🏻‍❤‍💋‍👨🏼|👩🏻‍❤‍💋‍👨🏽|👩🏾‍❤‍💋‍👨🏾|👩🏾‍❤‍💋‍👨🏿|👩🏾‍❤‍💋‍👨🏻|👩🏾‍❤‍💋‍👨🏼|👩🏾‍❤‍💋‍👨🏽|👩🏼‍❤‍💋‍👨🏼|👩🏼‍❤‍💋‍👨🏿|👩🏼‍❤‍💋‍👨🏻|👩🏼‍❤‍💋‍👨🏾|👩🏼‍❤‍💋‍👨🏽|👩🏽‍❤‍💋‍👨🏽|👩🏽‍❤‍💋‍👨🏿|👩🏽‍❤‍💋‍👨🏻|👩🏽‍❤‍💋‍👨🏾|👩🏽‍❤‍💋‍👨🏼|👩🏿‍❤‍💋‍👩🏿|👩🏿‍❤‍💋‍👩🏻|👩🏿‍❤‍💋‍👩🏾|👩🏿‍❤‍💋‍👩🏼|👩🏿‍❤‍💋‍👩🏽|👩🏻‍❤‍💋‍👩🏻|👩🏻‍❤‍💋‍👩🏿|👩🏻‍❤‍💋‍👩🏾|👩🏻‍❤‍💋‍👩🏼|👩🏻‍❤‍💋‍👩🏽|👩🏾‍❤‍💋‍👩🏾|👩🏾‍❤‍💋‍👩🏿|👩🏾‍❤‍💋‍👩🏻|👩🏾‍❤‍💋‍👩🏼|👩🏾‍❤‍💋‍👩🏽|👩🏼‍❤‍💋‍👩🏼|👩🏼‍❤‍💋‍👩🏿|👩🏼‍❤‍💋‍👩🏻|👩🏼‍❤‍💋‍👩🏾|👩🏼‍❤‍💋‍👩🏽|👩🏽‍❤‍💋‍👩🏽|👩🏽‍❤‍💋‍👩🏿|👩🏽‍❤‍💋‍👩🏻|👩🏽‍❤‍💋‍👩🏾|👩🏽‍❤‍💋‍👩🏼|👨🏿‍❤️‍👨🏿|👨🏿‍❤️‍👨🏻|👨🏿‍❤️‍👨🏾|👨🏿‍❤️‍👨🏼|👨🏿‍❤️‍👨🏽|👨🏻‍❤️‍👨🏻|👨🏻‍❤️‍👨🏿|👨🏻‍❤️‍👨🏾|👨🏻‍❤️‍👨🏼|👨🏻‍❤️‍👨🏽|👨🏾‍❤️‍👨🏾|👨🏾‍❤️‍👨🏿|👨🏾‍❤️‍👨🏻|👨🏾‍❤️‍👨🏼|👨🏾‍❤️‍👨🏽|👨🏼‍❤️‍👨🏼|👨🏼‍❤️‍👨🏿|👨🏼‍❤️‍👨🏻|👨🏼‍❤️‍👨🏾|👨🏼‍❤️‍👨🏽|👨🏽‍❤️‍👨🏽|👨🏽‍❤️‍👨🏿|👨🏽‍❤️‍👨🏻|👨🏽‍❤️‍👨🏾|👨🏽‍❤️‍👨🏼|🧑🏿‍❤️‍🧑🏻|🧑🏿‍❤️‍🧑🏾|🧑🏿‍❤️‍🧑🏼|🧑🏿‍❤️‍🧑🏽|🧑🏻‍❤️‍🧑🏿|🧑🏻‍❤️‍🧑🏾|🧑🏻‍❤️‍🧑🏼|🧑🏻‍❤️‍🧑🏽|🧑🏾‍❤️‍🧑🏿|🧑🏾‍❤️‍🧑🏻|🧑🏾‍❤️‍🧑🏼|🧑🏾‍❤️‍🧑🏽|🧑🏼‍❤️‍🧑🏿|🧑🏼‍❤️‍🧑🏻|🧑🏼‍❤️‍🧑🏾|🧑🏼‍❤️‍🧑🏽|🧑🏽‍❤️‍🧑🏿|🧑🏽‍❤️‍🧑🏻|🧑🏽‍❤️‍🧑🏾|🧑🏽‍❤️‍🧑🏼|👩🏿‍❤️‍👨🏿|👩🏿‍❤️‍👨🏻|👩🏿‍❤️‍👨🏾|👩🏿‍❤️‍👨🏼|👩🏿‍❤️‍👨🏽|👩🏻‍❤️‍👨🏻|👩🏻‍❤️‍👨🏿|👩🏻‍❤️‍👨🏾|👩🏻‍❤️‍👨🏼|👩🏻‍❤️‍👨🏽|👩🏾‍❤️‍👨🏾|👩🏾‍❤️‍👨🏿|👩🏾‍❤️‍👨🏻|👩🏾‍❤️‍👨🏼|👩🏾‍❤️‍👨🏽|👩🏼‍❤️‍👨🏼|👩🏼‍❤️‍👨🏿|👩🏼‍❤️‍👨🏻|👩🏼‍❤️‍👨🏾|👩🏼‍❤️‍👨🏽|👩🏽‍❤️‍👨🏽|👩🏽‍❤️‍👨🏿|👩🏽‍❤️‍👨🏻|👩🏽‍❤️‍👨🏾|👩🏽‍❤️‍👨🏼|👩🏿‍❤️‍👩🏿|👩🏿‍❤️‍👩🏻|👩🏿‍❤️‍👩🏾|👩🏿‍❤️‍👩🏼|👩🏿‍❤️‍👩🏽|👩🏻‍❤️‍👩🏻|👩🏻‍❤️‍👩🏿|👩🏻‍❤️‍👩🏾|👩🏻‍❤️‍👩🏼|👩🏻‍❤️‍👩🏽|👩🏾‍❤️‍👩🏾|👩🏾‍❤️‍👩🏿|👩🏾‍❤️‍👩🏻|👩🏾‍❤️‍👩🏼|👩🏾‍❤️‍👩🏽|👩🏼‍❤️‍👩🏼|👩🏼‍❤️‍👩🏿|👩🏼‍❤️‍👩🏻|👩🏼‍❤️‍👩🏾|👩🏼‍❤️‍👩🏽|👩🏽‍❤️‍👩🏽|👩🏽‍❤️‍👩🏿|👩🏽‍❤️‍👩🏻|👩🏽‍❤️‍👩🏾|👩🏽‍❤️‍👩🏼|👨‍❤️‍💋‍👨|👩‍❤️‍💋‍👨|👩‍❤️‍💋‍👩|🏴󠁧󠁢󠁥󠁮󠁧󠁿|🏴󠁧󠁢󠁳󠁣󠁴󠁿|🏴󠁧󠁢󠁷󠁬󠁳󠁿|👨🏿‍❤‍👨🏿|👨🏿‍❤‍👨🏻|👨🏿‍❤‍👨🏾|👨🏿‍❤‍👨🏼|👨🏿‍❤‍👨🏽|👨🏻‍❤‍👨🏻|👨🏻‍❤‍👨🏿|👨🏻‍❤‍👨🏾|👨🏻‍❤‍👨🏼|👨🏻‍❤‍👨🏽|👨🏾‍❤‍👨🏾|👨🏾‍❤‍👨🏿|👨🏾‍❤‍👨🏻|👨🏾‍❤‍👨🏼|👨🏾‍❤‍👨🏽|👨🏼‍❤‍👨🏼|👨🏼‍❤‍👨🏿|👨🏼‍❤‍👨🏻|👨🏼‍❤‍👨🏾|👨🏼‍❤‍👨🏽|👨🏽‍❤‍👨🏽|👨🏽‍❤‍👨🏿|👨🏽‍❤‍👨🏻|👨🏽‍❤‍👨🏾|👨🏽‍❤‍👨🏼|🧑🏿‍❤‍🧑🏻|🧑🏿‍❤‍🧑🏾|🧑🏿‍❤‍🧑🏼|🧑🏿‍❤‍🧑🏽|🧑🏻‍❤‍🧑🏿|🧑🏻‍❤‍🧑🏾|🧑🏻‍❤‍🧑🏼|🧑🏻‍❤‍🧑🏽|🧑🏾‍❤‍🧑🏿|🧑🏾‍❤‍🧑🏻|🧑🏾‍❤‍🧑🏼|🧑🏾‍❤‍🧑🏽|🧑🏼‍❤‍🧑🏿|🧑🏼‍❤‍🧑🏻|🧑🏼‍❤‍🧑🏾|🧑🏼‍❤‍🧑🏽|🧑🏽‍❤‍🧑🏿|🧑🏽‍❤‍🧑🏻|🧑🏽‍❤‍🧑🏾|🧑🏽‍❤‍🧑🏼|👩🏿‍❤‍👨🏿|👩🏿‍❤‍👨🏻|👩🏿‍❤‍👨🏾|👩🏿‍❤‍👨🏼|👩🏿‍❤‍👨🏽|👩🏻‍❤‍👨🏻|👩🏻‍❤‍👨🏿|👩🏻‍❤‍👨🏾|👩🏻‍❤‍👨🏼|👩🏻‍❤‍👨🏽|👩🏾‍❤‍👨🏾|👩🏾‍❤‍👨🏿|👩🏾‍❤‍👨🏻|👩🏾‍❤‍👨🏼|👩🏾‍❤‍👨🏽|👩🏼‍❤‍👨🏼|👩🏼‍❤‍👨🏿|👩🏼‍❤‍👨🏻|👩🏼‍❤‍👨🏾|👩🏼‍❤‍👨🏽|👩🏽‍❤‍👨🏽|👩🏽‍❤‍👨🏿|👩🏽‍❤‍👨🏻|👩🏽‍❤‍👨🏾|👩🏽‍❤‍👨🏼|👩🏿‍❤‍👩🏿|👩🏿‍❤‍👩🏻|👩🏿‍❤‍👩🏾|👩🏿‍❤‍👩🏼|👩🏿‍❤‍👩🏽|👩🏻‍❤‍👩🏻|👩🏻‍❤‍👩🏿|👩🏻‍❤‍👩🏾|👩🏻‍❤‍👩🏼|👩🏻‍❤‍👩🏽|👩🏾‍❤‍👩🏾|👩🏾‍❤‍👩🏿|👩🏾‍❤‍👩🏻|👩🏾‍❤‍👩🏼|👩🏾‍❤‍👩🏽|👩🏼‍❤‍👩🏼|👩🏼‍❤‍👩🏿|👩🏼‍❤‍👩🏻|👩🏼‍❤‍👩🏾|👩🏼‍❤‍👩🏽|👩🏽‍❤‍👩🏽|👩🏽‍❤‍👩🏿|👩🏽‍❤‍👩🏻|👩🏽‍❤‍👩🏾|👩🏽‍❤‍👩🏼|👨‍👨‍👦‍👦|👨‍👨‍👧‍👦|👨‍👨‍👧‍👧|👨‍👩‍👦‍👦|👨‍👩‍👧‍👦|👨‍👩‍👧‍👧|👩‍👩‍👦‍👦|👩‍👩‍👧‍👦|👩‍👩‍👧‍👧|👨‍❤‍💋‍👨|👩‍❤‍💋‍👨|👩‍❤‍💋‍👩|👨🏿‍🤝‍👨🏻|👨🏿‍🤝‍👨🏾|👨🏿‍🤝‍👨🏼|👨🏿‍🤝‍👨🏽|👨🏻‍🤝‍👨🏿|👨🏻‍🤝‍👨🏾|👨🏻‍🤝‍👨🏼|👨🏻‍🤝‍👨🏽|👨🏾‍🤝‍👨🏿|👨🏾‍🤝‍👨🏻|👨🏾‍🤝‍👨🏼|👨🏾‍🤝‍👨🏽|👨🏼‍🤝‍👨🏿|👨🏼‍🤝‍👨🏻|👨🏼‍🤝‍👨🏾|👨🏼‍🤝‍👨🏽|👨🏽‍🤝‍👨🏿|👨🏽‍🤝‍👨🏻|👨🏽‍🤝‍👨🏾|👨🏽‍🤝‍👨🏼|🧑🏿‍🤝‍🧑🏿|🧑🏿‍🤝‍🧑🏻|🧑🏿‍🤝‍🧑🏾|🧑🏿‍🤝‍🧑🏼|🧑🏿‍🤝‍🧑🏽|🧑🏻‍🤝‍🧑🏻|🧑🏻‍🤝‍🧑🏿|🧑🏻‍🤝‍🧑🏾|🧑🏻‍🤝‍🧑🏼|🧑🏻‍🤝‍🧑🏽|🧑🏾‍🤝‍🧑🏾|🧑🏾‍🤝‍🧑🏿|🧑🏾‍🤝‍🧑🏻|🧑🏾‍🤝‍🧑🏼|🧑🏾‍🤝‍🧑🏽|🧑🏼‍🤝‍🧑🏼|🧑🏼‍🤝‍🧑🏿|🧑🏼‍🤝‍🧑🏻|🧑🏼‍🤝‍🧑🏾|🧑🏼‍🤝‍🧑🏽|🧑🏽‍🤝‍🧑🏽|🧑🏽‍🤝‍🧑🏿|🧑🏽‍🤝‍🧑🏻|🧑🏽‍🤝‍🧑🏾|🧑🏽‍🤝‍🧑🏼|👩🏿‍🤝‍👨🏻|👩🏿‍🤝‍👨🏾|👩🏿‍🤝‍👨🏼|👩🏿‍🤝‍👨🏽|👩🏻‍🤝‍👨🏿|👩🏻‍🤝‍👨🏾|👩🏻‍🤝‍👨🏼|👩🏻‍🤝‍👨🏽|👩🏾‍🤝‍👨🏿|👩🏾‍🤝‍👨🏻|👩🏾‍🤝‍👨🏼|👩🏾‍🤝‍👨🏽|👩🏼‍🤝‍👨🏿|👩🏼‍🤝‍👨🏻|👩🏼‍🤝‍👨🏾|👩🏼‍🤝‍👨🏽|👩🏽‍🤝‍👨🏿|👩🏽‍🤝‍👨🏻|👩🏽‍🤝‍👨🏾|👩🏽‍🤝‍👨🏼|👩🏿‍🤝‍👩🏻|👩🏿‍🤝‍👩🏾|👩🏿‍🤝‍👩🏼|👩🏿‍🤝‍👩🏽|👩🏻‍🤝‍👩🏿|👩🏻‍🤝‍👩🏾|👩🏻‍🤝‍👩🏼|👩🏻‍🤝‍👩🏽|👩🏾‍🤝‍👩🏿|👩🏾‍🤝‍👩🏻|👩🏾‍🤝‍👩🏼|👩🏾‍🤝‍👩🏽|👩🏼‍🤝‍👩🏿|👩🏼‍🤝‍👩🏻|👩🏼‍🤝‍👩🏾|👩🏼‍🤝‍👩🏽|👩🏽‍🤝‍👩🏿|👩🏽‍🤝‍👩🏻|👩🏽‍🤝‍👩🏾|👩🏽‍🤝‍👩🏼|👨‍❤️‍👨|👩‍❤️‍👨|👩‍❤️‍👩|👨‍❤‍👨|👩‍❤‍👨|👩‍❤‍👩|🧏🏿‍♂️|🧏🏻‍♂️|🧏🏾‍♂️|🧏🏼‍♂️|🧏🏽‍♂️|🧏🏿‍♀️|🧏🏻‍♀️|🧏🏾‍♀️|🧏🏼‍♀️|🧏🏽‍♀️|👁️‍🗨️|👨‍👦‍👦|👨‍👧‍👦|👨‍👧‍👧|👨‍👨‍👦|👨‍👨‍👧|👨‍👩‍👦|👨‍👩‍👧|👩‍👦‍👦|👩‍👧‍👦|👩‍👧‍👧|👩‍👩‍👦|👩‍👩‍👧|🫱🏿‍🫲🏻|🫱🏿‍🫲🏾|🫱🏿‍🫲🏼|🫱🏿‍🫲🏽|🫱🏻‍🫲🏿|🫱🏻‍🫲🏾|🫱🏻‍🫲🏼|🫱🏻‍🫲🏽|🫱🏾‍🫲🏿|🫱🏾‍🫲🏻|🫱🏾‍🫲🏼|🫱🏾‍🫲🏽|🫱🏼‍🫲🏿|🫱🏼‍🫲🏻|🫱🏼‍🫲🏾|🫱🏼‍🫲🏽|🫱🏽‍🫲🏿|🫱🏽‍🫲🏻|🫱🏽‍🫲🏾|🫱🏽‍🫲🏼|🧑🏿‍⚕️|🧑🏻‍⚕️|🧑🏾‍⚕️|🧑🏼‍⚕️|🧑🏽‍⚕️|🧑🏿‍⚖️|🧑🏻‍⚖️|🧑🏾‍⚖️|🧑🏼‍⚖️|🧑🏽‍⚖️|🚴🏿‍♂️|🚴🏻‍♂️|🚴🏾‍♂️|🚴🏼‍♂️|🚴🏽‍♂️|⛹️‍♂️|⛹🏿‍♂️|⛹🏻‍♂️|⛹🏾‍♂️|⛹🏼‍♂️|⛹🏽‍♂️|🙇🏿‍♂️|🙇🏻‍♂️|🙇🏾‍♂️|🙇🏼‍♂️|🙇🏽‍♂️|🤸🏿‍♂️|🤸🏻‍♂️|🤸🏾‍♂️|🤸🏼‍♂️|🤸🏽‍♂️|🧗🏿‍♂️|🧗🏻‍♂️|🧗🏾‍♂️|🧗🏼‍♂️|🧗🏽‍♂️|👷🏿‍♂️|👷🏻‍♂️|👷🏾‍♂️|👷🏼‍♂️|👷🏽‍♂️|🧔🏿‍♂️|👱🏿‍♂️|🕵️‍♂️|🕵🏿‍♂️|🕵🏻‍♂️|🕵🏾‍♂️|🕵🏼‍♂️|🕵🏽‍♂️|🧝🏿‍♂️|🧝🏻‍♂️|🧝🏾‍♂️|🧝🏼‍♂️|🧝🏽‍♂️|🤦🏿‍♂️|🤦🏻‍♂️|🤦🏾‍♂️|🤦🏼‍♂️|🤦🏽‍♂️|🧚🏿‍♂️|🧚🏻‍♂️|🧚🏾‍♂️|🧚🏼‍♂️|🧚🏽‍♂️|🙍🏿‍♂️|🙍🏻‍♂️|🙍🏾‍♂️|🙍🏼‍♂️|🙍🏽‍♂️|🙅🏿‍♂️|🙅🏻‍♂️|🙅🏾‍♂️|🙅🏼‍♂️|🙅🏽‍♂️|🙆🏿‍♂️|🙆🏻‍♂️|🙆🏾‍♂️|🙆🏼‍♂️|🙆🏽‍♂️|💇🏿‍♂️|💇🏻‍♂️|💇🏾‍♂️|💇🏼‍♂️|💇🏽‍♂️|💆🏿‍♂️|💆🏻‍♂️|💆🏾‍♂️|💆🏼‍♂️|💆🏽‍♂️|🏌️‍♂️|🏌🏿‍♂️|🏌🏻‍♂️|🏌🏾‍♂️|🏌🏼‍♂️|🏌🏽‍♂️|💂🏿‍♂️|💂🏻‍♂️|💂🏾‍♂️|💂🏼‍♂️|💂🏽‍♂️|👨🏿‍⚕️|👨🏻‍⚕️|👨🏾‍⚕️|👨🏼‍⚕️|👨🏽‍⚕️|🧘🏿‍♂️|🧘🏻‍♂️|🧘🏾‍♂️|🧘🏼‍♂️|🧘🏽‍♂️|🧖🏿‍♂️|🧖🏻‍♂️|🧖🏾‍♂️|🧖🏼‍♂️|🧖🏽‍♂️|🤵🏿‍♂️|🤵🏻‍♂️|🤵🏾‍♂️|🤵🏼‍♂️|🤵🏽‍♂️|👨🏿‍⚖️|👨🏻‍⚖️|👨🏾‍⚖️|👨🏼‍⚖️|👨🏽‍⚖️|🤹🏿‍♂️|🤹🏻‍♂️|🤹🏾‍♂️|🤹🏼‍♂️|🤹🏽‍♂️|🧎🏿‍♂️|🧎🏻‍♂️|🧎🏾‍♂️|🧎🏼‍♂️|🧎🏽‍♂️|🏋️‍♂️|🏋🏿‍♂️|🏋🏻‍♂️|🏋🏾‍♂️|🏋🏼‍♂️|🏋🏽‍♂️|🧔🏻‍♂️|👱🏻‍♂️|🧙🏿‍♂️|🧙🏻‍♂️|🧙🏾‍♂️|🧙🏼‍♂️|🧙🏽‍♂️|🧔🏾‍♂️|👱🏾‍♂️|🧔🏼‍♂️|👱🏼‍♂️|🧔🏽‍♂️|👱🏽‍♂️|🚵🏿‍♂️|🚵🏻‍♂️|🚵🏾‍♂️|🚵🏼‍♂️|🚵🏽‍♂️|👨🏿‍✈️|👨🏻‍✈️|👨🏾‍✈️|👨🏼‍✈️|👨🏽‍✈️|🤾🏿‍♂️|🤾🏻‍♂️|🤾🏾‍♂️|🤾🏼‍♂️|🤾🏽‍♂️|🤽🏿‍♂️|🤽🏻‍♂️|🤽🏾‍♂️|🤽🏼‍♂️|🤽🏽‍♂️|👮🏿‍♂️|👮🏻‍♂️|👮🏾‍♂️|👮🏼‍♂️|👮🏽‍♂️|🙎🏿‍♂️|🙎🏻‍♂️|🙎🏾‍♂️|🙎🏼‍♂️|🙎🏽‍♂️|🙋🏿‍♂️|🙋🏻‍♂️|🙋🏾‍♂️|🙋🏼‍♂️|🙋🏽‍♂️|🚣🏿‍♂️|🚣🏻‍♂️|🚣🏾‍♂️|🚣🏼‍♂️|🚣🏽‍♂️|🏃🏿‍♂️|🏃🏻‍♂️|🏃🏾‍♂️|🏃🏼‍♂️|🏃🏽‍♂️|🤷🏿‍♂️|🤷🏻‍♂️|🤷🏾‍♂️|🤷🏼‍♂️|🤷🏽‍♂️|🧍🏿‍♂️|🧍🏻‍♂️|🧍🏾‍♂️|🧍🏼‍♂️|🧍🏽‍♂️|🦸🏿‍♂️|🦸🏻‍♂️|🦸🏾‍♂️|🦸🏼‍♂️|🦸🏽‍♂️|🦹🏿‍♂️|🦹🏻‍♂️|🦹🏾‍♂️|🦹🏼‍♂️|🦹🏽‍♂️|🏄🏿‍♂️|🏄🏻‍♂️|🏄🏾‍♂️|🏄🏼‍♂️|🏄🏽‍♂️|🏊🏿‍♂️|🏊🏻‍♂️|🏊🏾‍♂️|🏊🏼‍♂️|🏊🏽‍♂️|💁🏿‍♂️|💁🏻‍♂️|💁🏾‍♂️|💁🏼‍♂️|💁🏽‍♂️|🧛🏿‍♂️|🧛🏻‍♂️|🧛🏾‍♂️|🧛🏼‍♂️|🧛🏽‍♂️|🚶🏿‍♂️|🚶🏻‍♂️|🚶🏾‍♂️|🚶🏼‍♂️|🚶🏽‍♂️|👳🏿‍♂️|👳🏻‍♂️|👳🏾‍♂️|👳🏼‍♂️|👳🏽‍♂️|👰🏿‍♂️|👰🏻‍♂️|👰🏾‍♂️|👰🏼‍♂️|👰🏽‍♂️|🧜🏿‍♀️|🧜🏻‍♀️|🧜🏾‍♀️|🧜🏼‍♀️|🧜🏽‍♀️|🧜🏿‍♂️|🧜🏻‍♂️|🧜🏾‍♂️|🧜🏼‍♂️|🧜🏽‍♂️|🧑‍🤝‍🧑|🧑🏿‍✈️|🧑🏻‍✈️|🧑🏾‍✈️|🧑🏼‍✈️|🧑🏽‍✈️|🏳️‍⚧️|🚴🏿‍♀️|🚴🏻‍♀️|🚴🏾‍♀️|🚴🏼‍♀️|🚴🏽‍♀️|⛹️‍♀️|⛹🏿‍♀️|⛹🏻‍♀️|⛹🏾‍♀️|⛹🏼‍♀️|⛹🏽‍♀️|🙇🏿‍♀️|🙇🏻‍♀️|🙇🏾‍♀️|🙇🏼‍♀️|🙇🏽‍♀️|🤸🏿‍♀️|🤸🏻‍♀️|🤸🏾‍♀️|🤸🏼‍♀️|🤸🏽‍♀️|🧗🏿‍♀️|🧗🏻‍♀️|🧗🏾‍♀️|🧗🏼‍♀️|🧗🏽‍♀️|👷🏿‍♀️|👷🏻‍♀️|👷🏾‍♀️|👷🏼‍♀️|👷🏽‍♀️|🧔🏿‍♀️|👱🏿‍♀️|🕵️‍♀️|🕵🏿‍♀️|🕵🏻‍♀️|🕵🏾‍♀️|🕵🏼‍♀️|🕵🏽‍♀️|🧝🏿‍♀️|🧝🏻‍♀️|🧝🏾‍♀️|🧝🏼‍♀️|🧝🏽‍♀️|🤦🏿‍♀️|🤦🏻‍♀️|🤦🏾‍♀️|🤦🏼‍♀️|🤦🏽‍♀️|🧚🏿‍♀️|🧚🏻‍♀️|🧚🏾‍♀️|🧚🏼‍♀️|🧚🏽‍♀️|🙍🏿‍♀️|🙍🏻‍♀️|🙍🏾‍♀️|🙍🏼‍♀️|🙍🏽‍♀️|🙅🏿‍♀️|🙅🏻‍♀️|🙅🏾‍♀️|🙅🏼‍♀️|🙅🏽‍♀️|🙆🏿‍♀️|🙆🏻‍♀️|🙆🏾‍♀️|🙆🏼‍♀️|🙆🏽‍♀️|💇🏿‍♀️|💇🏻‍♀️|💇🏾‍♀️|💇🏼‍♀️|💇🏽‍♀️|💆🏿‍♀️|💆🏻‍♀️|💆🏾‍♀️|💆🏼‍♀️|💆🏽‍♀️|🏌️‍♀️|🏌🏿‍♀️|🏌🏻‍♀️|🏌🏾‍♀️|🏌🏼‍♀️|🏌🏽‍♀️|💂🏿‍♀️|💂🏻‍♀️|💂🏾‍♀️|💂🏼‍♀️|💂🏽‍♀️|👩🏿‍⚕️|👩🏻‍⚕️|👩🏾‍⚕️|👩🏼‍⚕️|👩🏽‍⚕️|🧘🏿‍♀️|🧘🏻‍♀️|🧘🏾‍♀️|🧘🏼‍♀️|🧘🏽‍♀️|🧖🏿‍♀️|🧖🏻‍♀️|🧖🏾‍♀️|🧖🏼‍♀️|🧖🏽‍♀️|🤵🏿‍♀️|🤵🏻‍♀️|🤵🏾‍♀️|🤵🏼‍♀️|🤵🏽‍♀️|👩🏿‍⚖️|👩🏻‍⚖️|👩🏾‍⚖️|👩🏼‍⚖️|👩🏽‍⚖️|🤹🏿‍♀️|🤹🏻‍♀️|🤹🏾‍♀️|🤹🏼‍♀️|🤹🏽‍♀️|🧎🏿‍♀️|🧎🏻‍♀️|🧎🏾‍♀️|🧎🏼‍♀️|🧎🏽‍♀️|🏋️‍♀️|🏋🏿‍♀️|🏋🏻‍♀️|🏋🏾‍♀️|🏋🏼‍♀️|🏋🏽‍♀️|🧔🏻‍♀️|👱🏻‍♀️|🧙🏿‍♀️|🧙🏻‍♀️|🧙🏾‍♀️|🧙🏼‍♀️|🧙🏽‍♀️|🧔🏾‍♀️|👱🏾‍♀️|🧔🏼‍♀️|👱🏼‍♀️|🧔🏽‍♀️|👱🏽‍♀️|🚵🏿‍♀️|🚵🏻‍♀️|🚵🏾‍♀️|🚵🏼‍♀️|🚵🏽‍♀️|👩🏿‍✈️|👩🏻‍✈️|👩🏾‍✈️|👩🏼‍✈️|👩🏽‍✈️|🤾🏿‍♀️|🤾🏻‍♀️|🤾🏾‍♀️|🤾🏼‍♀️|🤾🏽‍♀️|🤽🏿‍♀️|🤽🏻‍♀️|🤽🏾‍♀️|🤽🏼‍♀️|🤽🏽‍♀️|👮🏿‍♀️|👮🏻‍♀️|👮🏾‍♀️|👮🏼‍♀️|👮🏽‍♀️|🙎🏿‍♀️|🙎🏻‍♀️|🙎🏾‍♀️|🙎🏼‍♀️|🙎🏽‍♀️|🙋🏿‍♀️|🙋🏻‍♀️|🙋🏾‍♀️|🙋🏼‍♀️|🙋🏽‍♀️|🚣🏿‍♀️|🚣🏻‍♀️|🚣🏾‍♀️|🚣🏼‍♀️|🚣🏽‍♀️|🏃🏿‍♀️|🏃🏻‍♀️|🏃🏾‍♀️|🏃🏼‍♀️|🏃🏽‍♀️|🤷🏿‍♀️|🤷🏻‍♀️|🤷🏾‍♀️|🤷🏼‍♀️|🤷🏽‍♀️|🧍🏿‍♀️|🧍🏻‍♀️|🧍🏾‍♀️|🧍🏼‍♀️|🧍🏽‍♀️|🦸🏿‍♀️|🦸🏻‍♀️|🦸🏾‍♀️|🦸🏼‍♀️|🦸🏽‍♀️|🦹🏿‍♀️|🦹🏻‍♀️|🦹🏾‍♀️|🦹🏼‍♀️|🦹🏽‍♀️|🏄🏿‍♀️|🏄🏻‍♀️|🏄🏾‍♀️|🏄🏼‍♀️|🏄🏽‍♀️|🏊🏿‍♀️|🏊🏻‍♀️|🏊🏾‍♀️|🏊🏼‍♀️|🏊🏽‍♀️|💁🏿‍♀️|💁🏻‍♀️|💁🏾‍♀️|💁🏼‍♀️|💁🏽‍♀️|🧛🏿‍♀️|🧛🏻‍♀️|🧛🏾‍♀️|🧛🏼‍♀️|🧛🏽‍♀️|🚶🏿‍♀️|🚶🏻‍♀️|🚶🏾‍♀️|🚶🏼‍♀️|🚶🏽‍♀️|👳🏿‍♀️|👳🏻‍♀️|👳🏾‍♀️|👳🏼‍♀️|👳🏽‍♀️|👰🏿‍♀️|👰🏻‍♀️|👰🏾‍♀️|👰🏼‍♀️|👰🏽‍♀️|🧑🏿‍🎨|🧑🏻‍🎨|🧑🏾‍🎨|🧑🏼‍🎨|🧑🏽‍🎨|🧑🏿‍🚀|🧑🏻‍🚀|🧑🏾‍🚀|🧑🏼‍🚀|🧑🏽‍🚀|🧑🏿‍🍳|🧑🏻‍🍳|🧑🏾‍🍳|🧑🏼‍🍳|🧑🏽‍🍳|🧏‍♂️|🧏🏿‍♂|🧏🏻‍♂|🧏🏾‍♂|🧏🏼‍♂|🧏🏽‍♂|🧏‍♀️|🧏🏿‍♀|🧏🏻‍♀|🧏🏾‍♀|🧏🏼‍♀|🧏🏽‍♀|👁‍🗨️|👁️‍🗨|😶‍🌫️|🧑🏿‍🏭|🧑🏻‍🏭|🧑🏾‍🏭|🧑🏼‍🏭|🧑🏽‍🏭|🧑🏿‍🌾|🧑🏻‍🌾|🧑🏾‍🌾|🧑🏼‍🌾|🧑🏽‍🌾|🧑🏿‍🚒|🧑🏻‍🚒|🧑🏾‍🚒|🧑🏼‍🚒|🧑🏽‍🚒|🧑‍⚕️|🧑🏿‍⚕|🧑🏻‍⚕|🧑🏾‍⚕|🧑🏼‍⚕|🧑🏽‍⚕|❤️‍🔥|🧑‍⚖️|🧑🏿‍⚖|🧑🏻‍⚖|🧑🏾‍⚖|🧑🏼‍⚖|🧑🏽‍⚖|👨🏿‍🎨|👨🏻‍🎨|👨🏾‍🎨|👨🏼‍🎨|👨🏽‍🎨|👨🏿‍🚀|👨🏻‍🚀|👨🏾‍🚀|👨🏼‍🚀|👨🏽‍🚀|🧔‍♂️|🚴‍♂️|🚴🏿‍♂|🚴🏻‍♂|🚴🏾‍♂|🚴🏼‍♂|🚴🏽‍♂|👱‍♂️|⛹‍♂️|⛹️‍♂|⛹🏿‍♂|⛹🏻‍♂|⛹🏾‍♂|⛹🏼‍♂|⛹🏽‍♂|🙇‍♂️|🙇🏿‍♂|🙇🏻‍♂|🙇🏾‍♂|🙇🏼‍♂|🙇🏽‍♂|🤸‍♂️|🤸🏿‍♂|🤸🏻‍♂|🤸🏾‍♂|🤸🏼‍♂|🤸🏽‍♂|🧗‍♂️|🧗🏿‍♂|🧗🏻‍♂|🧗🏾‍♂|🧗🏼‍♂|🧗🏽‍♂|👷‍♂️|👷🏿‍♂|👷🏻‍♂|👷🏾‍♂|👷🏼‍♂|👷🏽‍♂|👨🏿‍🍳|👨🏻‍🍳|👨🏾‍🍳|👨🏼‍🍳|👨🏽‍🍳|👨🏿‍🦲|🧔🏿‍♂|👱🏿‍♂|👨🏿‍🦱|👨🏿‍🦰|👨🏿‍🦳|🕵‍♂️|🕵️‍♂|🕵🏿‍♂|🕵🏻‍♂|🕵🏾‍♂|🕵🏼‍♂|🕵🏽‍♂|🧝‍♂️|🧝🏿‍♂|🧝🏻‍♂|🧝🏾‍♂|🧝🏼‍♂|🧝🏽‍♂|🤦‍♂️|🤦🏿‍♂|🤦🏻‍♂|🤦🏾‍♂|🤦🏼‍♂|🤦🏽‍♂|👨🏿‍🏭|👨🏻‍🏭|👨🏾‍🏭|👨🏼‍🏭|👨🏽‍🏭|🧚‍♂️|🧚🏿‍♂|🧚🏻‍♂|🧚🏾‍♂|🧚🏼‍♂|🧚🏽‍♂|👨🏿‍🌾|👨🏻‍🌾|👨🏾‍🌾|👨🏼‍🌾|👨🏽‍🌾|👨🏿‍🍼|👨🏻‍🍼|👨🏾‍🍼|👨🏼‍🍼|👨🏽‍🍼|👨🏿‍🚒|👨🏻‍🚒|👨🏾‍🚒|👨🏼‍🚒|👨🏽‍🚒|🙍‍♂️|🙍🏿‍♂|🙍🏻‍♂|🙍🏾‍♂|🙍🏼‍♂|🙍🏽‍♂|🧞‍♂️|🙅‍♂️|🙅🏿‍♂|🙅🏻‍♂|🙅🏾‍♂|🙅🏼‍♂|🙅🏽‍♂|🙆‍♂️|🙆🏿‍♂|🙆🏻‍♂|🙆🏾‍♂|🙆🏼‍♂|🙆🏽‍♂|💇‍♂️|💇🏿‍♂|💇🏻‍♂|💇🏾‍♂|💇🏼‍♂|💇🏽‍♂|💆‍♂️|💆🏿‍♂|💆🏻‍♂|💆🏾‍♂|💆🏼‍♂|💆🏽‍♂|🏌‍♂️|🏌️‍♂|🏌🏿‍♂|🏌🏻‍♂|🏌🏾‍♂|🏌🏼‍♂|🏌🏽‍♂|💂‍♂️|💂🏿‍♂|💂🏻‍♂|💂🏾‍♂|💂🏼‍♂|💂🏽‍♂|👨‍⚕️|👨🏿‍⚕|👨🏻‍⚕|👨🏾‍⚕|👨🏼‍⚕|👨🏽‍⚕|🧘‍♂️|🧘🏿‍♂|🧘🏻‍♂|🧘🏾‍♂|🧘🏼‍♂|🧘🏽‍♂|👨🏿‍🦽|👨🏻‍🦽|👨🏾‍🦽|👨🏼‍🦽|👨🏽‍🦽|👨🏿‍🦼|👨🏻‍🦼|👨🏾‍🦼|👨🏼‍🦼|👨🏽‍🦼|🧖‍♂️|🧖🏿‍♂|🧖🏻‍♂|🧖🏾‍♂|🧖🏼‍♂|🧖🏽‍♂|🤵‍♂️|🤵🏿‍♂|🤵🏻‍♂|🤵🏾‍♂|🤵🏼‍♂|🤵🏽‍♂|👨‍⚖️|👨🏿‍⚖|👨🏻‍⚖|👨🏾‍⚖|👨🏼‍⚖|👨🏽‍⚖|🤹‍♂️|🤹🏿‍♂|🤹🏻‍♂|🤹🏾‍♂|🤹🏼‍♂|🤹🏽‍♂|🧎‍♂️|🧎🏿‍♂|🧎🏻‍♂|🧎🏾‍♂|🧎🏼‍♂|🧎🏽‍♂|🏋‍♂️|🏋️‍♂|🏋🏿‍♂|🏋🏻‍♂|🏋🏾‍♂|🏋🏼‍♂|🏋🏽‍♂|👨🏻‍🦲|🧔🏻‍♂|👱🏻‍♂|👨🏻‍🦱|👨🏻‍🦰|👨🏻‍🦳|🧙‍♂️|🧙🏿‍♂|🧙🏻‍♂|🧙🏾‍♂|🧙🏼‍♂|🧙🏽‍♂|👨🏿‍🔧|👨🏻‍🔧|👨🏾‍🔧|👨🏼‍🔧|👨🏽‍🔧|👨🏾‍🦲|🧔🏾‍♂|👱🏾‍♂|👨🏾‍🦱|👨🏾‍🦰|👨🏾‍🦳|👨🏼‍🦲|🧔🏼‍♂|👱🏼‍♂|👨🏼‍🦱|👨🏼‍🦰|👨🏼‍🦳|👨🏽‍🦲|🧔🏽‍♂|👱🏽‍♂|👨🏽‍🦱|👨🏽‍🦰|👨🏽‍🦳|🚵‍♂️|🚵🏿‍♂|🚵🏻‍♂|🚵🏾‍♂|🚵🏼‍♂|🚵🏽‍♂|👨🏿‍💼|👨🏻‍💼|👨🏾‍💼|👨🏼‍💼|👨🏽‍💼|👨‍✈️|👨🏿‍✈|👨🏻‍✈|👨🏾‍✈|👨🏼‍✈|👨🏽‍✈|🤾‍♂️|🤾🏿‍♂|🤾🏻‍♂|🤾🏾‍♂|🤾🏼‍♂|🤾🏽‍♂|🤽‍♂️|🤽🏿‍♂|🤽🏻‍♂|🤽🏾‍♂|🤽🏼‍♂|🤽🏽‍♂|👮‍♂️|👮🏿‍♂|👮🏻‍♂|👮🏾‍♂|👮🏼‍♂|👮🏽‍♂|🙎‍♂️|🙎🏿‍♂|🙎🏻‍♂|🙎🏾‍♂|🙎🏼‍♂|🙎🏽‍♂|🙋‍♂️|🙋🏿‍♂|🙋🏻‍♂|🙋🏾‍♂|🙋🏼‍♂|🙋🏽‍♂|🚣‍♂️|🚣🏿‍♂|🚣🏻‍♂|🚣🏾‍♂|🚣🏼‍♂|🚣🏽‍♂|🏃‍♂️|🏃🏿‍♂|🏃🏻‍♂|🏃🏾‍♂|🏃🏼‍♂|🏃🏽‍♂|👨🏿‍🔬|👨🏻‍🔬|👨🏾‍🔬|👨🏼‍🔬|👨🏽‍🔬|🤷‍♂️|🤷🏿‍♂|🤷🏻‍♂|🤷🏾‍♂|🤷🏼‍♂|🤷🏽‍♂|👨🏿‍🎤|👨🏻‍🎤|👨🏾‍🎤|👨🏼‍🎤|👨🏽‍🎤|🧍‍♂️|🧍🏿‍♂|🧍🏻‍♂|🧍🏾‍♂|🧍🏼‍♂|🧍🏽‍♂|👨🏿‍🎓|👨🏻‍🎓|👨🏾‍🎓|👨🏼‍🎓|👨🏽‍🎓|🦸‍♂️|🦸🏿‍♂|🦸🏻‍♂|🦸🏾‍♂|🦸🏼‍♂|🦸🏽‍♂|🦹‍♂️|🦹🏿‍♂|🦹🏻‍♂|🦹🏾‍♂|🦹🏼‍♂|🦹🏽‍♂|🏄‍♂️|🏄🏿‍♂|🏄🏻‍♂|🏄🏾‍♂|🏄🏼‍♂|🏄🏽‍♂|🏊‍♂️|🏊🏿‍♂|🏊🏻‍♂|🏊🏾‍♂|🏊🏼‍♂|🏊🏽‍♂|👨🏿‍🏫|👨🏻‍🏫|👨🏾‍🏫|👨🏼‍🏫|👨🏽‍🏫|👨🏿‍💻|👨🏻‍💻|👨🏾‍💻|👨🏼‍💻|👨🏽‍💻|💁‍♂️|💁🏿‍♂|💁🏻‍♂|💁🏾‍♂|💁🏼‍♂|💁🏽‍♂|🧛‍♂️|🧛🏿‍♂|🧛🏻‍♂|🧛🏾‍♂|🧛🏼‍♂|🧛🏽‍♂|🚶‍♂️|🚶🏿‍♂|🚶🏻‍♂|🚶🏾‍♂|🚶🏼‍♂|🚶🏽‍♂|👳‍♂️|👳🏿‍♂|👳🏻‍♂|👳🏾‍♂|👳🏼‍♂|👳🏽‍♂|👰‍♂️|👰🏿‍♂|👰🏻‍♂|👰🏾‍♂|👰🏼‍♂|👰🏽‍♂|👨🏿‍🦯|👨🏻‍🦯|👨🏾‍🦯|👨🏼‍🦯|👨🏽‍🦯|🧟‍♂️|🧑🏿‍🔧|🧑🏻‍🔧|🧑🏾‍🔧|🧑🏼‍🔧|🧑🏽‍🔧|👯‍♂️|🤼‍♂️|❤️‍🩹|🧜‍♀️|🧜🏿‍♀|🧜🏻‍♀|🧜🏾‍♀|🧜🏼‍♀|🧜🏽‍♀|🧜‍♂️|🧜🏿‍♂|🧜🏻‍♂|🧜🏾‍♂|🧜🏼‍♂|🧜🏽‍♂|🧑🏿‍🎄|🧑🏻‍🎄|🧑🏾‍🎄|🧑🏼‍🎄|🧑🏽‍🎄|🧑🏿‍💼|🧑🏻‍💼|🧑🏾‍💼|🧑🏼‍💼|🧑🏽‍💼|🧑🏿‍🦲|🧑🏿‍🦱|🧑🏿‍🦰|🧑🏿‍🦳|🧑🏿‍🍼|🧑🏻‍🍼|🧑🏾‍🍼|🧑🏼‍🍼|🧑🏽‍🍼|🧑🏿‍🦽|🧑🏻‍🦽|🧑🏾‍🦽|🧑🏼‍🦽|🧑🏽‍🦽|🧑🏿‍🦼|🧑🏻‍🦼|🧑🏾‍🦼|🧑🏼‍🦼|🧑🏽‍🦼|🧑🏻‍🦲|🧑🏻‍🦱|🧑🏻‍🦰|🧑🏻‍🦳|🧑🏾‍🦲|🧑🏾‍🦱|🧑🏾‍🦰|🧑🏾‍🦳|🧑🏼‍🦲|🧑🏼‍🦱|🧑🏼‍🦰|🧑🏼‍🦳|🧑🏽‍🦲|🧑🏽‍🦱|🧑🏽‍🦰|🧑🏽‍🦳|🧑🏿‍🦯|🧑🏻‍🦯|🧑🏾‍🦯|🧑🏼‍🦯|🧑🏽‍🦯|🧑‍✈️|🧑🏿‍✈|🧑🏻‍✈|🧑🏾‍✈|🧑🏼‍✈|🧑🏽‍✈|🏴‍☠️|🐻‍❄️|🏳️‍🌈|🧑🏿‍🔬|🧑🏻‍🔬|🧑🏾‍🔬|🧑🏼‍🔬|🧑🏽‍🔬|🧑🏿‍🎤|🧑🏻‍🎤|🧑🏾‍🎤|🧑🏼‍🎤|🧑🏽‍🎤|🧑🏿‍🎓|🧑🏻‍🎓|🧑🏾‍🎓|🧑🏼‍🎓|🧑🏽‍🎓|🧑🏿‍🏫|🧑🏻‍🏫|🧑🏾‍🏫|🧑🏼‍🏫|🧑🏽‍🏫|🧑🏿‍💻|🧑🏻‍💻|🧑🏾‍💻|🧑🏼‍💻|🧑🏽‍💻|🏳‍⚧️|🏳️‍⚧|👩🏿‍🎨|👩🏻‍🎨|👩🏾‍🎨|👩🏼‍🎨|👩🏽‍🎨|👩🏿‍🚀|👩🏻‍🚀|👩🏾‍🚀|👩🏼‍🚀|👩🏽‍🚀|🧔‍♀️|🚴‍♀️|🚴🏿‍♀|🚴🏻‍♀|🚴🏾‍♀|🚴🏼‍♀|🚴🏽‍♀|👱‍♀️|⛹‍♀️|⛹️‍♀|⛹🏿‍♀|⛹🏻‍♀|⛹🏾‍♀|⛹🏼‍♀|⛹🏽‍♀|🙇‍♀️|🙇🏿‍♀|🙇🏻‍♀|🙇🏾‍♀|🙇🏼‍♀|🙇🏽‍♀|🤸‍♀️|🤸🏿‍♀|🤸🏻‍♀|🤸🏾‍♀|🤸🏼‍♀|🤸🏽‍♀|🧗‍♀️|🧗🏿‍♀|🧗🏻‍♀|🧗🏾‍♀|🧗🏼‍♀|🧗🏽‍♀|👷‍♀️|👷🏿‍♀|👷🏻‍♀|👷🏾‍♀|👷🏼‍♀|👷🏽‍♀|👩🏿‍🍳|👩🏻‍🍳|👩🏾‍🍳|👩🏼‍🍳|👩🏽‍🍳|👩🏿‍🦲|🧔🏿‍♀|👱🏿‍♀|👩🏿‍🦱|👩🏿‍🦰|👩🏿‍🦳|🕵‍♀️|🕵️‍♀|🕵🏿‍♀|🕵🏻‍♀|🕵🏾‍♀|🕵🏼‍♀|🕵🏽‍♀|🧝‍♀️|🧝🏿‍♀|🧝🏻‍♀|🧝🏾‍♀|🧝🏼‍♀|🧝🏽‍♀|🤦‍♀️|🤦🏿‍♀|🤦🏻‍♀|🤦🏾‍♀|🤦🏼‍♀|🤦🏽‍♀|👩🏿‍🏭|👩🏻‍🏭|👩🏾‍🏭|👩🏼‍🏭|👩🏽‍🏭|🧚‍♀️|🧚🏿‍♀|🧚🏻‍♀|🧚🏾‍♀|🧚🏼‍♀|🧚🏽‍♀|👩🏿‍🌾|👩🏻‍🌾|👩🏾‍🌾|👩🏼‍🌾|👩🏽‍🌾|👩🏿‍🍼|👩🏻‍🍼|👩🏾‍🍼|👩🏼‍🍼|👩🏽‍🍼|👩🏿‍🚒|👩🏻‍🚒|👩🏾‍🚒|👩🏼‍🚒|👩🏽‍🚒|🙍‍♀️|🙍🏿‍♀|🙍🏻‍♀|🙍🏾‍♀|🙍🏼‍♀|🙍🏽‍♀|🧞‍♀️|🙅‍♀️|🙅🏿‍♀|🙅🏻‍♀|🙅🏾‍♀|🙅🏼‍♀|🙅🏽‍♀|🙆‍♀️|🙆🏿‍♀|🙆🏻‍♀|🙆🏾‍♀|🙆🏼‍♀|🙆🏽‍♀|💇‍♀️|💇🏿‍♀|💇🏻‍♀|💇🏾‍♀|💇🏼‍♀|💇🏽‍♀|💆‍♀️|💆🏿‍♀|💆🏻‍♀|💆🏾‍♀|💆🏼‍♀|💆🏽‍♀|🏌‍♀️|🏌️‍♀|🏌🏿‍♀|🏌🏻‍♀|🏌🏾‍♀|🏌🏼‍♀|🏌🏽‍♀|💂‍♀️|💂🏿‍♀|💂🏻‍♀|💂🏾‍♀|💂🏼‍♀|💂🏽‍♀|👩‍⚕️|👩🏿‍⚕|👩🏻‍⚕|👩🏾‍⚕|👩🏼‍⚕|👩🏽‍⚕|🧘‍♀️|🧘🏿‍♀|🧘🏻‍♀|🧘🏾‍♀|🧘🏼‍♀|🧘🏽‍♀|👩🏿‍🦽|👩🏻‍🦽|👩🏾‍🦽|👩🏼‍🦽|👩🏽‍🦽|👩🏿‍🦼|👩🏻‍🦼|👩🏾‍🦼|👩🏼‍🦼|👩🏽‍🦼|🧖‍♀️|🧖🏿‍♀|🧖🏻‍♀|🧖🏾‍♀|🧖🏼‍♀|🧖🏽‍♀|🤵‍♀️|🤵🏿‍♀|🤵🏻‍♀|🤵🏾‍♀|🤵🏼‍♀|🤵🏽‍♀|👩‍⚖️|👩🏿‍⚖|👩🏻‍⚖|👩🏾‍⚖|👩🏼‍⚖|👩🏽‍⚖|🤹‍♀️|🤹🏿‍♀|🤹🏻‍♀|🤹🏾‍♀|🤹🏼‍♀|🤹🏽‍♀|🧎‍♀️|🧎🏿‍♀|🧎🏻‍♀|🧎🏾‍♀|🧎🏼‍♀|🧎🏽‍♀|🏋‍♀️|🏋️‍♀|🏋🏿‍♀|🏋🏻‍♀|🏋🏾‍♀|🏋🏼‍♀|🏋🏽‍♀|👩🏻‍🦲|🧔🏻‍♀|👱🏻‍♀|👩🏻‍🦱|👩🏻‍🦰|👩🏻‍🦳|🧙‍♀️|🧙🏿‍♀|🧙🏻‍♀|🧙🏾‍♀|🧙🏼‍♀|🧙🏽‍♀|👩🏿‍🔧|👩🏻‍🔧|👩🏾‍🔧|👩🏼‍🔧|👩🏽‍🔧|👩🏾‍🦲|🧔🏾‍♀|👱🏾‍♀|👩🏾‍🦱|👩🏾‍🦰|👩🏾‍🦳|👩🏼‍🦲|🧔🏼‍♀|👱🏼‍♀|👩🏼‍🦱|👩🏼‍🦰|👩🏼‍🦳|👩🏽‍🦲|🧔🏽‍♀|👱🏽‍♀|👩🏽‍🦱|👩🏽‍🦰|👩🏽‍🦳|🚵‍♀️|🚵🏿‍♀|🚵🏻‍♀|🚵🏾‍♀|🚵🏼‍♀|🚵🏽‍♀|👩🏿‍💼|👩🏻‍💼|👩🏾‍💼|👩🏼‍💼|👩🏽‍💼|👩‍✈️|👩🏿‍✈|👩🏻‍✈|👩🏾‍✈|👩🏼‍✈|👩🏽‍✈|🤾‍♀️|🤾🏿‍♀|🤾🏻‍♀|🤾🏾‍♀|🤾🏼‍♀|🤾🏽‍♀|🤽‍♀️|🤽🏿‍♀|🤽🏻‍♀|🤽🏾‍♀|🤽🏼‍♀|🤽🏽‍♀|👮‍♀️|👮🏿‍♀|👮🏻‍♀|👮🏾‍♀|👮🏼‍♀|👮🏽‍♀|🙎‍♀️|🙎🏿‍♀|🙎🏻‍♀|🙎🏾‍♀|🙎🏼‍♀|🙎🏽‍♀|🙋‍♀️|🙋🏿‍♀|🙋🏻‍♀|🙋🏾‍♀|🙋🏼‍♀|🙋🏽‍♀|🚣‍♀️|🚣🏿‍♀|🚣🏻‍♀|🚣🏾‍♀|🚣🏼‍♀|🚣🏽‍♀|🏃‍♀️|🏃🏿‍♀|🏃🏻‍♀|🏃🏾‍♀|🏃🏼‍♀|🏃🏽‍♀|👩🏿‍🔬|👩🏻‍🔬|👩🏾‍🔬|👩🏼‍🔬|👩🏽‍🔬|🤷‍♀️|🤷🏿‍♀|🤷🏻‍♀|🤷🏾‍♀|🤷🏼‍♀|🤷🏽‍♀|👩🏿‍🎤|👩🏻‍🎤|👩🏾‍🎤|👩🏼‍🎤|👩🏽‍🎤|🧍‍♀️|🧍🏿‍♀|🧍🏻‍♀|🧍🏾‍♀|🧍🏼‍♀|🧍🏽‍♀|👩🏿‍🎓|👩🏻‍🎓|👩🏾‍🎓|👩🏼‍🎓|👩🏽‍🎓|🦸‍♀️|🦸🏿‍♀|🦸🏻‍♀|🦸🏾‍♀|🦸🏼‍♀|🦸🏽‍♀|🦹‍♀️|🦹🏿‍♀|🦹🏻‍♀|🦹🏾‍♀|🦹🏼‍♀|🦹🏽‍♀|🏄‍♀️|🏄🏿‍♀|🏄🏻‍♀|🏄🏾‍♀|🏄🏼‍♀|🏄🏽‍♀|🏊‍♀️|🏊🏿‍♀|🏊🏻‍♀|🏊🏾‍♀|🏊🏼‍♀|🏊🏽‍♀|👩🏿‍🏫|👩🏻‍🏫|👩🏾‍🏫|👩🏼‍🏫|👩🏽‍🏫|👩🏿‍💻|👩🏻‍💻|👩🏾‍💻|👩🏼‍💻|👩🏽‍💻|💁‍♀️|💁🏿‍♀|💁🏻‍♀|💁🏾‍♀|💁🏼‍♀|💁🏽‍♀|🧛‍♀️|🧛🏿‍♀|🧛🏻‍♀|🧛🏾‍♀|🧛🏼‍♀|🧛🏽‍♀|🚶‍♀️|🚶🏿‍♀|🚶🏻‍♀|🚶🏾‍♀|🚶🏼‍♀|🚶🏽‍♀|👳‍♀️|👳🏿‍♀|👳🏻‍♀|👳🏾‍♀|👳🏼‍♀|👳🏽‍♀|👰‍♀️|👰🏿‍♀|👰🏻‍♀|👰🏾‍♀|👰🏼‍♀|👰🏽‍♀|👩🏿‍🦯|👩🏻‍🦯|👩🏾‍🦯|👩🏼‍🦯|👩🏽‍🦯|🧟‍♀️|👯‍♀️|🤼‍♀️|🧑‍🎨|🧑‍🚀|🐈‍⬛|🧑‍🍳|🧏‍♂|🧏‍♀|👁‍🗨|😮‍💨|😶‍🌫|😵‍💫|🧑‍🏭|👨‍👦|👨‍👧|👩‍👦|👩‍👧|🧑‍🌾|🧑‍🚒|🧑‍⚕|❤‍🔥|🧑‍⚖|\#️⃣|\*️⃣|0️⃣|1️⃣|2️⃣|3️⃣|4️⃣|5️⃣|6️⃣|7️⃣|8️⃣|9️⃣|👨‍🎨|👨‍🚀|👨‍🦲|🧔‍♂|🚴‍♂|👱‍♂|⛹‍♂|🙇‍♂|🤸‍♂|🧗‍♂|👷‍♂|👨‍🍳|👨‍🦱|🕵‍♂|🧝‍♂|🤦‍♂|👨‍🏭|🧚‍♂|👨‍🌾|👨‍🍼|👨‍🚒|🙍‍♂|🧞‍♂|🙅‍♂|🙆‍♂|💇‍♂|💆‍♂|🏌‍♂|💂‍♂|👨‍⚕|🧘‍♂|👨‍🦽|👨‍🦼|🧖‍♂|🤵‍♂|👨‍⚖|🤹‍♂|🧎‍♂|🏋‍♂|🧙‍♂|👨‍🔧|🚵‍♂|👨‍💼|👨‍✈|🤾‍♂|🤽‍♂|👮‍♂|🙎‍♂|🙋‍♂|👨‍🦰|🚣‍♂|🏃‍♂|👨‍🔬|🤷‍♂|👨‍🎤|🧍‍♂|👨‍🎓|🦸‍♂|🦹‍♂|🏄‍♂|🏊‍♂|👨‍🏫|👨‍💻|💁‍♂|🧛‍♂|🚶‍♂|👳‍♂|👨‍🦳|👰‍♂|👨‍🦯|🧟‍♂|🧑‍🔧|👯‍♂|🤼‍♂|❤‍🩹|🧜‍♀|🧜‍♂|🧑‍🎄|🧑‍💼|🧑‍🦲|🧑‍🦱|🧑‍🍼|🧑‍🦽|🧑‍🦼|🧑‍🦰|🧑‍🦳|🧑‍🦯|🧑‍✈|🏴‍☠|🐻‍❄|🏳‍🌈|🧑‍🔬|🐕‍🦺|🧑‍🎤|🧑‍🎓|🧑‍🏫|🧑‍💻|🏳‍⚧|👩‍🎨|👩‍🚀|👩‍🦲|🧔‍♀|🚴‍♀|👱‍♀|⛹‍♀|🙇‍♀|🤸‍♀|🧗‍♀|👷‍♀|👩‍🍳|👩‍🦱|🕵‍♀|🧝‍♀|🤦‍♀|👩‍🏭|🧚‍♀|👩‍🌾|👩‍🍼|👩‍🚒|🙍‍♀|🧞‍♀|🙅‍♀|🙆‍♀|💇‍♀|💆‍♀|🏌‍♀|💂‍♀|👩‍⚕|🧘‍♀|👩‍🦽|👩‍🦼|🧖‍♀|🤵‍♀|👩‍⚖|🤹‍♀|🧎‍♀|🏋‍♀|🧙‍♀|👩‍🔧|🚵‍♀|👩‍💼|👩‍✈|🤾‍♀|🤽‍♀|👮‍♀|🙎‍♀|🙋‍♀|👩‍🦰|🚣‍♀|🏃‍♀|👩‍🔬|🤷‍♀|👩‍🎤|🧍‍♀|👩‍🎓|🦸‍♀|🦹‍♀|🏄‍♀|🏊‍♀|👩‍🏫|👩‍💻|💁‍♀|🧛‍♀|🚶‍♀|👳‍♀|👩‍🦳|👰‍♀|👩‍🦯|🧟‍♀|👯‍♀|🤼‍♀|🅰️|🇦🇫|🇦🇱|🇩🇿|🇦🇸|🇦🇩|🇦🇴|🇦🇮|🇦🇶|🇦🇬|🇦🇷|🇦🇲|🇦🇼|🇦🇨|🇦🇺|🇦🇹|🇦🇿|🅱️|🇧🇸|🇧🇭|🇧🇩|🇧🇧|🇧🇾|🇧🇪|🇧🇿|🇧🇯|🇧🇲|🇧🇹|🇧🇴|🇧🇦|🇧🇼|🇧🇻|🇧🇷|🇮🇴|🇻🇬|🇧🇳|🇧🇬|🇧🇫|🇧🇮|🇰🇭|🇨🇲|🇨🇦|🇮🇨|🇨🇻|🇧🇶|🇰🇾|🇨🇫|🇪🇦|🇹🇩|🇨🇱|🇨🇳|🇨🇽|🇨🇵|🇨🇨|🇨🇴|🇰🇲|🇨🇬|🇨🇩|🇨🇰|🇨🇷|🇭🇷|🇨🇺|🇨🇼|🇨🇾|🇨🇿|🇨🇮|🇩🇰|🇩🇬|🇩🇯|🇩🇲|🇩🇴|🇪🇨|🇪🇬|🇸🇻|🇬🇶|🇪🇷|🇪🇪|🇸🇿|🇪🇹|🇪🇺|🇫🇰|🇫🇴|🇫🇯|🇫🇮|🇫🇷|🇬🇫|🇵🇫|🇹🇫|🇬🇦|🇬🇲|🇬🇪|🇩🇪|🇬🇭|🇬🇮|🇬🇷|🇬🇱|🇬🇩|🇬🇵|🇬🇺|🇬🇹|🇬🇬|🇬🇳|🇬🇼|🇬🇾|🇭🇹|🇭🇲|🇭🇳|🇭🇰|🇭🇺|🇮🇸|🇮🇳|🇮🇩|🇮🇷|🇮🇶|🇮🇪|🇮🇲|🇮🇱|🇮🇹|🇯🇲|🇯🇵|㊗️|🈷️|㊙️|🈂️|🇯🇪|🇯🇴|🇰🇿|🇰🇪|🇰🇮|🇽🇰|🇰🇼|🇰🇬|🇱🇦|🇱🇻|🇱🇧|🇱🇸|🇱🇷|🇱🇾|🇱🇮|🇱🇹|🇱🇺|🇲🇴|🇲🇬|🇲🇼|🇲🇾|🇲🇻|🇲🇱|🇲🇹|🇲🇭|🇲🇶|🇲🇷|🇲🇺|🇾🇹|🇲🇽|🇫🇲|🇲🇩|🇲🇨|🇲🇳|🇲🇪|🇲🇸|🇲🇦|🇲🇿|🤶🏿|🤶🏻|🤶🏾|🤶🏼|🤶🏽|🇲🇲|🇳🇦|🇳🇷|🇳🇵|🇳🇱|🇳🇨|🇳🇿|🇳🇮|🇳🇪|🇳🇬|🇳🇺|🇳🇫|🇰🇵|🇲🇰|🇲🇵|🇳🇴|👌🏿|👌🏻|👌🏾|👌🏼|👌🏽|🅾️|🇴🇲|🅿️|🇵🇰|🇵🇼|🇵🇸|🇵🇦|🇵🇬|🇵🇾|🇵🇪|🇵🇭|🇵🇳|🇵🇱|🇵🇹|🇵🇷|🇶🇦|🇷🇴|🇷🇺|🇷🇼|🇷🇪|🇼🇸|🇸🇲|🎅🏿|🎅🏻|🎅🏾|🎅🏼|🎅🏽|🇸🇦|🇸🇳|🇷🇸|🇸🇨|🇸🇱|🇸🇬|🇸🇽|🇸🇰|🇸🇮|🇸🇧|🇸🇴|🇿🇦|🇬🇸|🇰🇷|🇸🇸|🇪🇸|🇱🇰|🇧🇱|🇸🇭|🇰🇳|🇱🇨|🇲🇫|🇵🇲|🇻🇨|🇸🇩|🇸🇷|🇸🇯|🇸🇪|🇨🇭|🇸🇾|🇸🇹|🇹🇼|🇹🇯|🇹🇿|🇹🇭|🇹🇱|🇹🇬|🇹🇰|🇹🇴|🇹🇹|🇹🇦|🇹🇳|🇹🇷|🇹🇲|🇹🇨|🇹🇻|🇺🇲|🇻🇮|🇺🇬|🇺🇦|🇦🇪|🇬🇧|🇺🇳|🇺🇸|🇺🇾|🇺🇿|🇻🇺|🇻🇦|🇻🇪|🇻🇳|🇼🇫|🇪🇭|🇾🇪|🇿🇲|🇿🇼|🎟️|✈️|⚗️|⚛️|👼🏿|👼🏻|👼🏾|👼🏼|👼🏽|👶🏿|👶🏻|👶🏾|👶🏼|👶🏽|👇🏿|👇🏻|👇🏾|👇🏼|👇🏽|👈🏿|👈🏻|👈🏾|👈🏼|👈🏽|👉🏿|👉🏻|👉🏾|👉🏼|👉🏽|👆🏿|👆🏻|👆🏾|👆🏼|👆🏽|⚖️|🗳️|🏖️|🛏️|🛎️|☣️|◼️|✒️|▪️|👦🏿|👦🏻|👦🏾|👦🏼|👦🏽|🤱🏿|🤱🏻|🤱🏾|🤱🏼|🤱🏽|🏗️|🤙🏿|🤙🏻|🤙🏾|🤙🏼|🤙🏽|🏕️|🕯️|🗃️|🗂️|⛓️|☑️|✔️|♟️|🧒🏿|🧒🏻|🧒🏾|🧒🏼|🧒🏽|🐿️|Ⓜ️|🏙️|🗜️|👏🏿|👏🏻|👏🏾|👏🏼|👏🏽|🏛️|☁️|🌩️|⛈️|🌧️|🌨️|♣️|⚰️|☄️|🖱️|👷🏿|👷🏻|👷🏾|👷🏼|👷🏽|🎛️|©️|🛋️|💑🏿|💑🏻|💑🏾|💑🏼|💑🏽|🖍️|🤞🏿|🤞🏻|🤞🏾|🤞🏼|🤞🏽|⚔️|🗡️|🧏🏿|🧏🏻|🧏🏾|🧏🏼|🧏🏽|🏚️|🏜️|🏝️|🖥️|🕵️|🕵🏿|🕵🏻|🕵🏾|🕵🏼|🕵🏽|♦️|‼️|🕊️|↙️|↘️|⬇️|👂🏿|👂🏻|👂🏾|👂🏼|👂🏽|🦻🏿|🦻🏻|🦻🏾|🦻🏼|🦻🏽|✴️|✳️|⏏️|🧝🏿|🧝🏻|🧝🏾|🧝🏼|🧝🏽|✉️|⁉️|👁️|🧚🏿|🧚🏻|🧚🏾|🧚🏼|🧚🏽|♀️|⛴️|🗄️|🎞️|📽️|⚜️|💪🏿|💪🏻|💪🏾|💪🏼|💪🏽|🌫️|🙏🏿|🙏🏻|🙏🏾|🙏🏼|🙏🏽|🦶🏿|🦶🏻|🦶🏾|🦶🏼|🦶🏽|🍽️|🖋️|🖼️|☹️|⚱️|⚙️|👧🏿|👧🏻|👧🏾|👧🏼|👧🏽|💂🏿|💂🏻|💂🏾|💂🏼|💂🏽|⚒️|🛠️|🖐️|🖐🏿|🖐🏻|🖐🏾|🖐🏼|🖐🏽|🫰🏿|🫰🏻|🫰🏾|🫰🏼|🫰🏽|🤝🏿|🤝🏻|🤝🏾|🤝🏼|🤝🏽|❣️|🫶🏿|🫶🏻|🫶🏾|🫶🏼|🫶🏽|♥️|🕳️|🏇🏿|🏇🏻|🏇🏾|🏇🏼|🏇🏽|🌶️|♨️|🏘️|⛸️|🫵🏿|🫵🏻|🫵🏾|🫵🏼|🫵🏽|☝️|☝🏿|☝🏻|☝🏾|☝🏼|☝🏽|♾️|ℹ️|🕹️|⌨️|\#⃣|\*⃣|0⃣|1⃣|2⃣|3⃣|4⃣|5⃣|6⃣|7⃣|8⃣|9⃣|💏🏿|💏🏻|💏🏾|💏🏼|💏🏽|🏷️|⏮️|✝️|🤛🏿|🤛🏻|🤛🏾|🤛🏼|🤛🏽|↔️|⬅️|↪️|🗨️|🫲🏿|🫲🏻|🫲🏾|🫲🏼|🫲🏽|🦵🏿|🦵🏻|🦵🏾|🦵🏼|🦵🏽|🎚️|🖇️|🤟🏿|🤟🏻|🤟🏾|🤟🏼|🤟🏽|🧙🏿|🧙🏻|🧙🏾|🧙🏼|🧙🏽|♂️|🕺🏿|🕺🏻|🕺🏾|🕺🏼|🕺🏽|👨🏿|👨🏻|👨🏾|👨🏼|👨🏽|🕰️|⚕️|👬🏿|👬🏻|👬🏾|👬🏼|👬🏽|🧜🏿|🧜🏻|🧜🏾|🧜🏼|🧜🏽|🖕🏿|🖕🏻|🖕🏾|🖕🏼|🖕🏽|🎖️|🛥️|🏍️|🛣️|⛰️|✖️|💅🏿|💅🏻|💅🏾|💅🏼|💅🏽|🏞️|⏭️|🥷🏿|🥷🏻|🥷🏾|🥷🏼|🥷🏽|👃🏿|👃🏻|👃🏾|👃🏼|👃🏽|🛢️|🗝️|👴🏿|👴🏻|👴🏾|👴🏼|👴🏽|👵🏿|👵🏻|👵🏾|👵🏼|👵🏽|🧓🏿|🧓🏻|🧓🏾|🧓🏼|🧓🏽|🕉️|👊🏿|👊🏻|👊🏾|👊🏼|👊🏽|👐🏿|👐🏻|👐🏾|👐🏼|👐🏽|☦️|🖌️|🫳🏿|🫳🏻|🫳🏾|🫳🏼|🫳🏽|🫴🏿|🫴🏻|🫴🏾|🫴🏼|🫴🏽|🤲🏿|🤲🏻|🤲🏾|🤲🏼|🤲🏽|〽️|🛳️|⏸️|☮️|🖊️|✏️|🚴🏿|🚴🏻|🚴🏾|🚴🏼|🚴🏽|⛹️|⛹🏿|⛹🏻|⛹🏾|⛹🏼|⛹🏽|🙇🏿|🙇🏻|🙇🏾|🙇🏼|🙇🏽|🤸🏿|🤸🏻|🤸🏾|🤸🏼|🤸🏽|🧗🏿|🧗🏻|🧗🏾|🧗🏼|🧗🏽|🧑🏿|🧔🏿|👱🏿|🤦🏿|🤦🏻|🤦🏾|🤦🏼|🤦🏽|🙍🏿|🙍🏻|🙍🏾|🙍🏼|🙍🏽|🙅🏿|🙅🏻|🙅🏾|🙅🏼|🙅🏽|🙆🏿|🙆🏻|🙆🏾|🙆🏼|🙆🏽|💇🏿|💇🏻|💇🏾|💇🏼|💇🏽|💆🏿|💆🏻|💆🏾|💆🏼|💆🏽|🏌️|🏌🏿|🏌🏻|🏌🏾|🏌🏼|🏌🏽|🛌🏿|🛌🏻|🛌🏾|🛌🏼|🛌🏽|🧘🏿|🧘🏻|🧘🏾|🧘🏼|🧘🏽|🧖🏿|🧖🏻|🧖🏾|🧖🏼|🧖🏽|🕴️|🕴🏿|🕴🏻|🕴🏾|🕴🏼|🕴🏽|🤵🏿|🤵🏻|🤵🏾|🤵🏼|🤵🏽|🤹🏿|🤹🏻|🤹🏾|🤹🏼|🤹🏽|🧎🏿|🧎🏻|🧎🏾|🧎🏼|🧎🏽|🏋️|🏋🏿|🏋🏻|🏋🏾|🏋🏼|🏋🏽|🧑🏻|🧔🏻|👱🏻|🧑🏾|🧔🏾|👱🏾|🧑🏼|🧔🏼|👱🏼|🧑🏽|🧔🏽|👱🏽|🚵🏿|🚵🏻|🚵🏾|🚵🏼|🚵🏽|🤾🏿|🤾🏻|🤾🏾|🤾🏼|🤾🏽|🤽🏿|🤽🏻|🤽🏾|🤽🏼|🤽🏽|🙎🏿|🙎🏻|🙎🏾|🙎🏼|🙎🏽|🙋🏿|🙋🏻|🙋🏾|🙋🏼|🙋🏽|🚣🏿|🚣🏻|🚣🏾|🚣🏼|🚣🏽|🏃🏿|🏃🏻|🏃🏾|🏃🏼|🏃🏽|🤷🏿|🤷🏻|🤷🏾|🤷🏼|🤷🏽|🧍🏿|🧍🏻|🧍🏾|🧍🏼|🧍🏽|🏄🏿|🏄🏻|🏄🏾|🏄🏼|🏄🏽|🏊🏿|🏊🏻|🏊🏾|🏊🏼|🏊🏽|🛀🏿|🛀🏻|🛀🏾|🛀🏼|🛀🏽|💁🏿|💁🏻|💁🏾|💁🏼|💁🏽|🚶🏿|🚶🏻|🚶🏾|🚶🏼|🚶🏽|👳🏿|👳🏻|👳🏾|👳🏼|👳🏽|🫅🏿|🫅🏻|🫅🏾|🫅🏼|🫅🏽|👲🏿|👲🏻|👲🏾|👲🏼|👲🏽|👰🏿|👰🏻|👰🏾|👰🏼|👰🏽|⛏️|🤌🏿|🤌🏻|🤌🏾|🤌🏼|🤌🏽|🤏🏿|🤏🏻|🤏🏾|🤏🏼|🤏🏽|▶️|⏯️|👮🏿|👮🏻|👮🏾|👮🏼|👮🏽|🫃🏿|🫃🏻|🫃🏾|🫃🏼|🫃🏽|🫄🏿|🫄🏻|🫄🏾|🫄🏼|🫄🏽|🤰🏿|🤰🏻|🤰🏾|🤰🏼|🤰🏽|🤴🏿|🤴🏻|🤴🏾|🤴🏼|🤴🏽|👸🏿|👸🏻|👸🏾|👸🏼|👸🏽|🖨️|🏎️|☢️|🛤️|🤚🏿|🤚🏻|🤚🏾|🤚🏼|🤚🏽|✊🏿|✊🏻|✊🏾|✊🏼|✊🏽|✋🏿|✋🏻|✋🏾|✋🏼|✋🏽|🙌🏿|🙌🏻|🙌🏾|🙌🏼|🙌🏽|⏺️|♻️|❤️|®️|🎗️|⛑️|◀️|🤜🏿|🤜🏻|🤜🏾|🤜🏼|🤜🏽|🗯️|➡️|⤵️|↩️|⤴️|🫱🏿|🫱🏻|🫱🏾|🫱🏼|🫱🏽|🗞️|🏵️|🛰️|✂️|🤳🏿|🤳🏻|🤳🏾|🤳🏼|🤳🏽|☘️|🛡️|⛩️|🛍️|🤘🏿|🤘🏻|🤘🏾|🤘🏼|🤘🏽|⛷️|☠️|🛩️|☺️|🏔️|🏂🏿|🏂🏻|🏂🏾|🏂🏼|🏂🏽|❄️|☃️|♠️|❇️|🗣️|🕷️|🕸️|🗓️|🗒️|🏟️|☪️|✡️|⏹️|⏱️|🎙️|☀️|🌥️|🌦️|🌤️|🕶️|🦸🏿|🦸🏻|🦸🏾|🦸🏼|🦸🏽|🦹🏿|🦹🏻|🦹🏾|🦹🏼|🦹🏽|☎️|🌡️|👎🏿|👎🏻|👎🏾|👎🏼|👎🏽|👍🏿|👍🏻|👍🏾|👍🏼|👍🏽|⏲️|🌪️|🖲️|™️|⚧️|☂️|⛱️|↕️|↖️|↗️|⬆️|🧛🏿|🧛🏻|🧛🏾|🧛🏼|🧛🏽|✌️|✌🏿|✌🏻|✌🏾|✌🏼|✌🏽|🖖🏿|🖖🏻|🖖🏾|🖖🏼|🖖🏽|⚠️|🗑️|👋🏿|👋🏻|👋🏾|👋🏼|👋🏽|〰️|☸️|🏳️|◻️|▫️|🌬️|👫🏿|👫🏻|👫🏾|👫🏼|👫🏽|💃🏿|💃🏻|💃🏾|💃🏼|💃🏽|👩🏿|👩🏻|👩🏾|👩🏼|👩🏽|🧕🏿|🧕🏻|🧕🏾|🧕🏼|🧕🏽|👭🏿|👭🏻|👭🏾|👭🏼|👭🏽|🗺️|✍️|✍🏿|✍🏻|✍🏾|✍🏼|✍🏽|☯️|🇦🇽|🥇|🥈|🥉|🆎|🏧|🅰|♒|♈|🔙|🅱|🆑|🆒|♋|♑|🎄|🔚|🆓|♊|🆔|🉑|🈸|🉐|🏯|㊗|🈹|🎎|🈚|🈁|🈷|🈵|🈶|🈺|🈴|🏣|🈲|🈯|㊙|🈂|🔰|🈳|♌|♎|🤶|🆕|🆖|🆗|👌|🔛|🅾|⛎|🅿|♓|🔜|🆘|♐|🎅|♏|🗽|🦖|🔝|♉|🗼|🆙|🆚|♍|🧮|🪗|🩹|🎟|🚡|✈|🛬|🛫|⏰|⚗|👽|👾|🚑|🏈|🏺|🫀|⚓|💢|😠|👿|😧|🐜|📶|😰|🚛|🎨|😲|⚛|🛺|🚗|🥑|🪓|👶|👼|🍼|🐤|🚼|👇|👈|👉|👆|🎒|🥓|🦡|🏸|🥯|🛄|🥖|⚖|🦲|🩰|🎈|🗳|🍌|🪕|🏦|📊|💈|⚾|🧺|🏀|🦇|🛁|🔋|🏖|😁|🫘|🐻|💓|🦫|🛏|🍺|🪲|🔔|🫑|🔕|🛎|🍱|🧃|🚲|👙|🧢|☣|🐦|🎂|🦬|🫦|⚫|🏴|🖤|⬛|◾|◼|✒|▪|🔲|🌼|🐡|📘|🔵|💙|🟦|🫐|🐗|💣|🦴|🔖|📑|📚|🪃|🍾|💐|🏹|🥣|🎳|🥊|👦|🧠|🍞|🤱|🧱|🌉|💼|🩲|🔆|🥦|💔|🧹|🟤|🤎|🟫|🧋|🫧|🪣|🐛|🏗|🚅|🎯|🌯|🚌|🚏|👤|👥|🧈|🦋|🌵|📅|🤙|🐪|📷|📸|🏕|🕯|🍬|🥫|🛶|🗃|📇|🗂|🎠|🎏|🪚|🥕|🏰|🐈|🐱|😹|😼|⛓|🪑|📉|📈|💹|☑|✔|✅|🧀|🏁|🍒|🌸|♟|🌰|🐔|🧒|🚸|🐿|🍫|🥢|⛪|🚬|🎦|Ⓜ|🎪|🏙|🌆|🗜|🎬|👏|🏛|🍻|🥂|📋|🔃|📕|📪|📫|🌂|☁|🌩|⛈|🌧|🌨|🤡|♣|👝|🧥|🪳|🍸|🥥|⚰|🪙|🥶|💥|☄|🧭|💽|🖱|🎊|😖|😕|🚧|👷|🎛|🏪|🍚|🍪|🍳|©|🪸|🛋|🔄|💑|🐄|🐮|🤠|🦀|🖍|💳|🌙|🦗|🏏|🐊|🥐|❌|❎|🤞|🎌|⚔|👑|🩼|😿|😢|🔮|🥒|🥤|🧁|🥌|🦱|➰|💱|🍛|🍮|🛃|🥩|🌀|🗡|🍡|🏿|💨|🧏|🌳|🦌|🚚|🏬|🏚|🏜|🏝|🖥|🕵|♦|💠|🔅|😞|🥸|➗|🤿|🪔|💫|🧬|🦤|🐕|🐶|💵|🐬|🚪|🫥|🔯|➿|‼|🍩|🕊|↙|↘|⬇|😓|🔽|🐉|🐲|👗|🤤|🩸|💧|🥁|🦆|🥟|📀|📧|🦅|👂|🌽|🦻|🥚|🍆|✴|✳|🕣|🕗|⏏|🔌|🐘|🛗|🕦|🕚|🧝|🪹|✉|📩|💶|🌲|🐑|⁉|🤯|😑|👁|👀|😘|🥹|😋|😱|🤮|😵|🫤|🤭|🤕|😷|🧐|🫢|😮|🫣|🤨|🙄|😤|🤬|😂|🤒|😛|😶|🏭|🧚|🧆|🍂|👪|⏩|⏬|⏪|⏫|📠|😨|🪶|♀|🎡|⛴|🏑|🗄|📁|🎞|📽|🔥|🚒|🧯|🧨|🎆|🌓|🌛|🐟|🍥|🎣|🕠|🕔|⛳|🦩|🔦|🥿|🫓|⚜|💪|💾|🎴|😳|🪰|🥏|🛸|🌫|🌁|🙏|🫕|🦶|👣|🍴|🍽|🥠|⛲|🖋|🕟|🍀|🕓|🦊|🖼|🍟|🍤|🐸|🐥|☹|😦|⛽|🌕|🌝|⚱|🎲|🧄|⚙|💎|🧞|👻|🦒|👧|🥛|👓|🌎|🌏|🌍|🌐|🧤|🌟|🥅|🐐|👺|🥽|🦍|🎓|🍇|🍏|📗|🟢|💚|🥗|🟩|😬|😺|😸|😀|😃|😄|😅|😆|💗|💂|🦮|🎸|🍔|🔨|⚒|🛠|🪬|🐹|🖐|🫰|👜|🤝|🐣|🎧|🪦|🙉|💟|❣|🫶|♥|💘|💝|💲|🟰|🦔|🚁|🌿|🌺|👠|🚄|⚡|🥾|🛕|🦛|🕳|⭕|🍯|🐝|🪝|🚥|🐎|🐴|🏇|🏥|☕|🌭|🥵|🌶|♨|🏨|⌛|⏳|🏠|🏡|🏘|💯|😯|🛖|🧊|🍨|🏒|⛸|🪪|📥|📨|🫵|☝|♾|ℹ|🔤|🔡|🔠|🔢|🔣|🎃|🫙|👖|🃏|🕹|🕋|🦘|🔑|⌨|🔟|🛴|👘|💏|💋|😽|😗|😚|😙|🔪|🪁|🥝|🪢|🐨|🥼|🏷|🥍|🪜|🐞|💻|🔷|🔶|🌗|🌜|⏮|✝|🍃|🥬|📒|🤛|↔|⬅|↪|🛅|🗨|🫲|🦵|🍋|🐆|🎚|💡|🚈|🏻|🔗|🖇|🦁|💄|🚮|🦎|🦙|🦞|🔒|🔐|🔏|🚂|🍭|🪘|🧴|🪷|😭|📢|🤟|🏩|💌|🪫|🧳|🫁|🤥|🧙|🪄|🧲|🔍|🔎|🀄|♂|🦣|👨|🕺|🥭|🕰|🦽|👞|🗾|🍁|🥋|🧉|🍖|🦾|🦿|⚕|🏾|🏼|🏽|📣|🍈|🫠|📝|👬|🕎|🚹|🧜|🚇|🦠|🎤|🔬|🖕|🪖|🎖|🌌|🚐|➖|🪞|🪩|🗿|📱|📴|📲|🤑|💰|💸|🐒|🐵|🚝|🥮|🎑|🕌|🦟|🛥|🛵|🏍|🦼|🛣|🗻|⛰|🚠|🚞|🐁|🐭|🪤|👄|🎥|✖|🍄|🎹|🎵|🎶|🎼|🔇|💅|📛|🏞|🤢|🧿|👔|🤓|🪺|🪆|😐|🌑|🌚|📰|⏭|🌃|🕤|🕘|🥷|🚳|⛔|🚯|📵|🔞|🚷|🚭|🚱|👃|📓|📔|🔩|🐙|🍢|🏢|👹|🛢|🗝|👴|👵|🧓|🫒|🕉|🚘|🚍|👊|🚔|🚖|🩱|🕜|🕐|🧅|📖|📂|👐|📭|📬|💿|📙|🟠|🧡|🟧|🦧|☦|🦦|📤|🦉|🐂|🦪|📦|📄|📃|📟|🖌|🫳|🌴|🫴|🤲|🥞|🐼|📎|🪂|🦜|〽|🎉|🥳|🛳|🛂|⏸|🐾|☮|🍑|🦚|🥜|🍐|🖊|✏|🐧|😔|🫂|👯|🤼|🎭|😣|🧑|🧔|🚴|👱|⛹|🙇|🤸|🧗|🤦|🤺|🙍|🙅|🙆|💇|💆|🏌|🛌|🧘|🧖|🕴|🤵|🤹|🧎|🏋|🚵|🤾|🤽|🙎|🙋|🚣|🏃|🤷|🧍|🏄|🏊|🛀|💁|🚶|👳|🫅|👲|👰|🧫|⛏|🛻|🥧|🐖|🐷|🐽|💩|💊|🤌|🤏|🎍|🍍|🏓|🍕|🪅|🪧|🛐|▶|⏯|🛝|🥺|🪠|➕|🚓|🚨|👮|🐩|🎱|🍿|🏤|📯|📮|🍲|🚰|🥔|🪴|🍗|💷|🫗|😾|😡|📿|🫃|🫄|🤰|🥨|🤴|👸|🖨|🚫|🟣|💜|🟪|👛|📌|🧩|🐇|🐰|🦝|🏎|📻|🔘|☢|🚃|🛤|🌈|🤚|✊|✋|🙌|🐏|🐀|🪒|🧾|⏺|♻|🍎|🔴|🧧|❗|🦰|❤|🏮|❓|🟥|🔻|🔺|®|😌|🎗|🔁|🔂|⛑|🚻|◀|💞|🦏|🎀|🍙|🍘|🤜|🗯|➡|⤵|↩|⤴|🫱|💍|🛟|🪐|🍠|🤖|🪨|🚀|🧻|🗞|🎢|🛼|🤣|🐓|🌹|🏵|📍|🏉|🎽|👟|😥|🧷|🦺|⛵|🍶|🧂|🫡|🥪|🥻|🛰|📡|🦕|🎷|🧣|🏫|✂|🦂|🪛|📜|🦭|💺|🙈|🌱|🤳|🕢|🕖|🪡|🥘|☘|🦈|🍧|🌾|🛡|⛩|🚢|🌠|🛍|🛒|🍰|🩳|🚿|🦐|🔀|🤫|🤘|🕡|🕕|🛹|⛷|🎿|💀|☠|🦨|🛷|😴|😪|🙁|🙂|🎰|🦥|🛩|🔹|🔸|😻|☺|😇|😍|🥰|😈|🤗|😊|😎|🥲|😏|🐌|🐍|🤧|🏔|🏂|❄|☃|⛄|🧼|⚽|🧦|🍦|🥎|♠|🍝|❇|🎇|✨|💖|🙊|🔊|🔈|🔉|🗣|💬|🚤|🕷|🕸|🗓|🗒|🐚|🧽|🥄|🚙|🏅|🐳|🦑|😝|🏟|⭐|🤩|☪|✡|🚉|🍜|🩺|⏹|🛑|⏱|📏|🍓|🎙|🥙|☀|⛅|🌥|🌦|🌤|🌞|🌻|🕶|🌅|🌄|🌇|🦸|🦹|🍣|🚟|🦢|💦|🕍|💉|👕|🌮|🥡|🫔|🎋|🍊|🚕|🍵|🫖|📆|🧸|☎|📞|🔭|📺|🕥|🕙|🎾|⛺|🧪|🌡|🤔|🩴|💭|🧵|🕞|🕒|👎|👍|🎫|🐅|🐯|⏲|😫|🚽|🍅|👅|🧰|🦷|🪥|🎩|🌪|🖲|🚜|™|🚆|🚊|🚋|⚧|🚩|📐|🔱|🧌|🚎|🏆|🍹|🐠|🎺|🌷|🥃|🦃|🐢|🕧|🕛|🐫|🕝|💕|🕑|☂|⛱|☔|😒|🦄|🔓|↕|↖|↗|⬆|🙃|🔼|🧛|🚦|📳|✌|📹|🎮|📼|🎻|🌋|🏐|🖖|🧇|🌘|🌖|⚠|🗑|⌚|🐃|🚾|🔫|🌊|🍉|👋|〰|🌒|🌔|🙀|😩|💒|🐋|🛞|☸|♿|🦯|⚪|❕|🏳|💮|🦳|🤍|⬜|◽|◻|❔|▫|🔳|🥀|🎐|🌬|🪟|🍷|😉|😜|🐺|👩|👫|💃|🧕|👢|👚|👒|👡|👭|🚺|🪵|🥴|🗺|🪱|😟|🎁|🔧|✍|🩻|🧶|🥱|🟡|💛|🟨|💴|☯|🪀|🤪|🦓|🤐|🧟|💤)");
        private static readonly Regex _customEmoteRegex = new(@"<a?:\w*:\d*>");

        public static bool Check(IMessage message, AutoModerationConfig config, IDiscordClient _)
        {
            if (config.Limit == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(message.Content))
            {
                return false;
            }

            int customEmotes = _customEmoteRegex.Matches(message.Content).Count;
            if (customEmotes > config.Limit)  // skip normal emote check if possible
            {
                return true;
            }

            int emotes = _emoteRegex.Matches(message.Content).Count;
            return (emotes + customEmotes) > config.Limit;
        }
    }
}