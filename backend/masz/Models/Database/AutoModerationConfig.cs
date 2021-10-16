using System.ComponentModel.DataAnnotations;
using masz.Dtos.AutoModerationConfig;
using masz.Enums;

namespace masz.Models
{
    public class AutoModerationConfig
    {
        [Key]
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public AutoModerationType AutoModerationType { get; set; }
        public AutoModerationAction AutoModerationAction { get; set; }
        public PunishmentType? PunishmentType { get; set; }
        public int? PunishmentDurationMinutes { get; set; }
        public ulong[] IgnoreChannels { get; set; }
        public ulong[] IgnoreRoles { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int? Limit { get; set; }
        public string CustomWordFilter { get; set; }
        public bool SendDmNotification { get; set; }
        public bool SendPublicNotification { get; set; }

        public AutoModerationConfig()
        {
        }

        public AutoModerationConfig(AutoModerationConfigForPutDto dto, ulong guildId)
        {
            GuildId = guildId;
            AutoModerationType = dto.AutoModerationType;
            AutoModerationAction = dto.AutoModerationAction;
            PunishmentType = dto.PunishmentType;
            PunishmentDurationMinutes = dto.PunishmentDurationMinutes;
            IgnoreChannels = dto.IgnoreChannels;
            IgnoreRoles = dto.IgnoreRoles;
            TimeLimitMinutes = dto.TimeLimitMinutes;
            Limit = dto.Limit;
            CustomWordFilter = dto.CustomWordFilter;
            SendDmNotification = dto.SendDmNotification;
            SendPublicNotification = dto.SendPublicNotification;
        }
    }
}