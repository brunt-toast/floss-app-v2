using System.ComponentModel.DataAnnotations;

namespace FlossApp.Application.Enums;

public enum SupportedLanguage
{
    [Display(Name= "English (United States)", Description = "English (United States)")]
    en_US,
        
    [Display(Name = "日本語（日本）", Description = "Japanese (Japan)")]
    ja_JA,
}
