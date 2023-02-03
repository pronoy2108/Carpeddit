﻿using Carpeddit.Api.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Carpeddit.App.ViewModels
{
    public sealed partial class RedditPrefsViewModel : ObservableObject
    {
        [ObservableProperty]
        [property: JsonPropertyName("accept_pms")]
        private string acceptPms;

        [ObservableProperty]
        [property: JsonPropertyName("bad_comment_autocollapse")]
        private string badCommentAutocollapse;

        [ObservableProperty]
        [property: JsonPropertyName("beta")]
        private bool beta;

        [ObservableProperty]
        [property: JsonPropertyName("collapse_read_messages")]
        private bool collapseReadMessages;

        [ObservableProperty]
        [property: JsonPropertyName("compress")]
        private bool compress;

        [ObservableProperty]
        [property: JsonPropertyName("country_code")]
        private string countryCode;

        [ObservableProperty]
        [property: JsonPropertyName("default_comment_sort")]
        private string defaultCommentSort;

        [ObservableProperty]
        [property: JsonPropertyName("email_chat_request")]
        private bool emailChatRequest;

        [ObservableProperty]
        [property: JsonPropertyName("email_comment_reply")]
        private bool emailCommentReply;

        [ObservableProperty]
        [property: JsonPropertyName("email_community_discovery")]
        private bool emailCommunityDiscovery;

        [ObservableProperty]
        [property: JsonPropertyName("email_digests")]
        private bool emailDigests;

        [ObservableProperty]
        [property: JsonPropertyName("email_messages")]
        private bool emailMessages;

        [ObservableProperty]
        [property: JsonPropertyName("email_new_user_welcome")]
        private bool emailNewUserWelcome;

        [ObservableProperty]
        [property: JsonPropertyName("email_post_reply")]
        private bool emailPostReply;

        [ObservableProperty]
        [property: JsonPropertyName("email_private_message")]
        private bool emailPrivateMessage;

        [ObservableProperty]
        [property: JsonPropertyName("email_unsubscribe_all")]
        private bool emailUnsubscribeAll;

        [ObservableProperty]
        [property: JsonPropertyName("email_upvote_comment")]
        private bool emailUpvoteComment;

        [ObservableProperty]
        [property: JsonPropertyName("email_upvote_post")]
        private bool emailUpvotePost;

        [ObservableProperty]
        [property: JsonPropertyName("email_user_new_follower")]
        private bool emailUserNewFollower;

        [ObservableProperty]
        [property: JsonPropertyName("email_username_mention")]
        private bool emailUsernameMention;

        [ObservableProperty]
        [property: JsonPropertyName("enable_followers")]
        private bool enableFollowers;

        [ObservableProperty]
        [property: JsonPropertyName("ignore_suggested_sort")]
        private bool ignoreSuggestedSort;

        [ObservableProperty]
        [property: JsonPropertyName("label_nsfw")]
        private bool labelNsfw;

        [ObservableProperty]
        [property: JsonPropertyName("lang")]
        private string lang;

        [ObservableProperty]
        [property: JsonPropertyName("legacy_search")]
        private bool legacySearch;

        [ObservableProperty]
        [property: JsonPropertyName("mark_messages_read")]
        private bool markMessagesRead;

        [ObservableProperty]
        [property: JsonPropertyName("media")]
        private string media;

        [ObservableProperty]
        [property: JsonPropertyName("media_preview")]
        private string mediaPreview;

        [ObservableProperty]
        [property: JsonPropertyName("num_comments")]
        private int numComments;

        [ObservableProperty]
        [property: JsonPropertyName("search_include_over_18")]
        private bool searchIncludeOver18;

        [ObservableProperty]
        [property: JsonPropertyName("send_crosspost_messages")]
        private bool sendCrosspostMessages;

        [ObservableProperty]
        [property: JsonPropertyName("send_welcome_messages")]
        private bool sendWelcomeMessages;

        [ObservableProperty]
        [property: JsonPropertyName("show_flair")]
        private bool showFlair;

        [ObservableProperty]
        [property: JsonPropertyName("show_link_flair")]
        private bool showLinkFlair;

        [ObservableProperty]
        [property: JsonPropertyName("show_location_based_recommendations")]
        private bool showLocationBasedRecommendations;

        [ObservableProperty]
        [property: JsonPropertyName("show_presence")]
        private bool showPresence;

        [ObservableProperty]
        [property: JsonPropertyName("show_trending")]
        private bool showTrending;

        [ObservableProperty]
        [property: JsonPropertyName("video_autoplay")]
        private bool videoAutoplay;

        public static async Task<RedditPrefsViewModel> GetForCurrentUserAsync()
        {
            try
            {
                return await WebHelper.GetDeserializedResponseAsync<RedditPrefsViewModel>("/api/v1/me/prefs", true);
            } catch (UnauthorizedAccessException)
            {
                await TokenHelper.Instance.RefreshTokenAsync(AccountHelper.Instance.GetCurrentInfo().RefreshToken);
                return await GetForCurrentUserAsync();
            }
        }

        public async Task UpdateAsync()
        {
            try
            {
                await WebHelper.PatchDeserializedResponseAsync<RedditPrefsViewModel>("/api/v1/me/prefs", GetPatchValues(), true);
            }
            catch (UnauthorizedAccessException)
            {
                await TokenHelper.Instance.RefreshTokenAsync(AccountHelper.Instance.GetCurrentInfo().RefreshToken);
                await UpdateAsync();
            }
        }

        private IDictionary<string, string> GetPatchValues()
        {
            var settings = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(AcceptPms))
                settings["accept_pms"] = AcceptPms;

            if (!string.IsNullOrEmpty(BadCommentAutocollapse))
                settings["bad_comment_autocollapse"] = BadCommentAutocollapse;

            if (!string.IsNullOrEmpty(CountryCode))
                settings["country_code"] = CountryCode;

            if (!string.IsNullOrEmpty(DefaultCommentSort))
                settings["default_comment_sort"] = DefaultCommentSort;

            if (!string.IsNullOrEmpty(Media))
                settings["media"] = Media;

            if (!string.IsNullOrEmpty(MediaPreview))
                settings["media_preview"] = MediaPreview;

            settings["beta"]                                = BoolToString(Beta);
            settings["collapse_read_messages"]              = BoolToString(CollapseReadMessages);
            settings["compress"]                            = BoolToString(Compress);
            settings["email_chat_request"]                  = BoolToString(EmailChatRequest);
            settings["email_comment_reply"]                 = BoolToString(EmailCommentReply);
            settings["email_community_discovery"]           = BoolToString(EmailCommunityDiscovery);
            settings["email_digests"]                       = BoolToString(EmailDigests);
            settings["email_messages"]                      = BoolToString(EmailMessages);
            settings["email_new_user_welcome"]              = BoolToString(EmailNewUserWelcome);
            settings["email_post_reply"]                    = BoolToString(EmailPostReply);
            settings["email_private_message"]               = BoolToString(EmailPrivateMessage);
            settings["email_unsubscribe_all"]               = BoolToString(EmailUnsubscribeAll);
            settings["email_upvote_comment"]                = BoolToString(EmailUpvoteComment);
            settings["email_upvote_post"]                   = BoolToString(EmailUpvotePost);
            settings["email_user_new_follower"]             = BoolToString(EmailUserNewFollower);
            settings["email_username_mention"]              = BoolToString(EmailUsernameMention);
            settings["enable_followers"]                    = BoolToString(EnableFollowers);
            settings["ignore_suggested_sort"]               = BoolToString(IgnoreSuggestedSort);
            settings["label_nsfw"]                          = BoolToString(LabelNsfw);
            settings["legacy_search"]                       = BoolToString(LegacySearch);
            settings["mark_messages_read"]                  = BoolToString(MarkMessagesRead);
            settings["search_include_over_18"]              = BoolToString(SearchIncludeOver18);
            settings["send_crosspost_messages"]             = BoolToString(SendCrosspostMessages);
            settings["send_welcome_messages"]               = BoolToString(SendWelcomeMessages);
            settings["show_flair"]                          = BoolToString(ShowFlair);
            settings["show_link_flair"]                     = BoolToString(ShowLinkFlair);
            settings["show_location_based_recommendations"] = BoolToString(ShowLocationBasedRecommendations);
            settings["show_presence"]                       = BoolToString(ShowPresence);
            settings["video_autoplay"]                      = BoolToString(VideoAutoplay);

            return settings;
        }

        private string BoolToString(bool value)
            => value ? "true" : "false";
    }
}