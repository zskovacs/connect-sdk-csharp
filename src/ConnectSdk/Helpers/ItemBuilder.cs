using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConnectSdk.Helpers;

/// <summary>
/// A builder for <see cref="FullItem"/>. 
/// </summary>
public sealed class ItemBuilder
{
    private FullItem _item;

    /// <summary>
    /// 
    /// </summary>
    public ItemBuilder()
    {
        Reset();
    }

    /// <summary>
    /// Clears accumulated properties and puts
    /// ItemBuilder back to a "pristine" state
    /// </summary>
    public void Reset()
    {
        _item = new FullItem
        {
            Tags = new HashSet<string>(), 
            Fields = new HashSet<Field>(),
            Sections = new HashSet<Section>()
        };
    }

    /// <summary>
    /// Assign category to the Item under construction.
    /// </summary>
    /// <param name="category">An <see cref="ItemCategory"/></param>
    /// <returns></returns>
    public ItemBuilder SetCategory(ItemCategory category)
    {
        _item.Category = category;
        return this;
    }

    /// <summary>
    /// Assign Vault to the Item under construction.
    /// </summary>
    /// <param name="vaultId">Id of the vault</param>
    /// <returns></returns>
    public ItemBuilder SetVault(string vaultId)
    {
        _item.Vault = new Vault { Id = vaultId };
        return this;
    }

    /// <summary>
    /// Assign Vault to the Item under construction.
    /// </summary>
    /// <param name="vault">A <see cref="Vault"/></param>
    /// <returns></returns>
    public ItemBuilder SetVault(Vault vault)
    {
        _item.Vault = vault;
        return this;
    }

    /// <summary>
    /// Assign Title to the Item under construction.
    /// </summary>
    /// <param name="title">Item title</param>
    /// <returns></returns>
    public ItemBuilder SetTitle(string title)
    {
        _item.Title = title;
        return this;
    }

    /// <summary>
    /// Add Tag to the Item under construction.
    /// </summary>
    /// <param name="tag">Tag of the item</param>
    /// <returns></returns>
    public ItemBuilder AddTag(string tag)
    {
        _item.Tags.Add(tag);
        return this;
    }

    /// <summary>
    /// Add Url to the Item under construction.
    /// </summary>
    /// <param name="url">An <see cref="Urls"/> object</param>
    /// <returns></returns>
    public ItemBuilder AddUrl(Urls url)
    {
        _item.Urls.Add(url);
        return this;
    }

    /// <summary>
    /// Append new Item Field to the in-flight Item.
    /// </summary>
    /// <param name="label"></param>
    /// <param name="value"></param>
    /// <param name="sectionName"></param>
    /// <param name="type"></param>
    /// <param name="purpose"></param>
    /// <param name="generate"></param>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public ItemBuilder AddField(string label, string value, string sectionName, FieldType? type = null, FieldPurpose? purpose = null, bool? generate = null, GeneratorRecipe recipe = null)
    {
        var field = new Field()
        {
            Type = type ?? FieldType.STRING,
            Purpose = purpose ?? FieldPurpose.Empty,
            Label = label,
            Value = value,
            Generate = generate ?? false,
            Recipe = generate == true && recipe is not null ? GenerateRecipe(recipe) : null
        };
        
        if (!string.IsNullOrWhiteSpace(sectionName)) {
            field.Section = GetOrCreateSection(sectionName);
        }

        return AddField(field);
    }

    /// <summary>
    /// Creates a well-formed GeneratorRecipe from the provided options.
    /// Namely, it removes duplicate values from the character set definitions.
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    private static GeneratorRecipe GenerateRecipe(GeneratorRecipe recipe)
    {
        // excluded character setting cannot contain duplicate entries
        var excludeCharacters = string.Concat(new HashSet<char>(recipe.ExcludeCharacters.ToArray()));

        return new GeneratorRecipe
        {
            CharacterSets = new List<CharacterSets>(new HashSet<CharacterSets>(recipe.CharacterSets)),
            ExcludeCharacters = excludeCharacters
        };
    }
    
    private Section GetOrCreateSection(string sectionName)
    {
        var normalizedName = NormalizeString(sectionName);

        var section = _item.Sections.SingleOrDefault(x => x.Label == normalizedName);
        if (section is not null)
        {
            return section;
        }

        section = new Section
        {
            Id = IdGenerator.GenerateSectionId(),
            Label = normalizedName
        };
        
        _item.Sections.Add(section);
        return section;
    }

    private static string NormalizeString(string input)
    {
        var normalized = input.ToLower();
        normalized = Regex.Replace(normalized, @"[*+~.()'"":@]", "");

        return normalized;
    }

    /// <summary>
    /// Append new Item Field to the in-flight Item.
    /// </summary>
    /// <param name="field">A <see cref="Field"/> object</param>
    /// <returns></returns>
    public ItemBuilder AddField(Field field)
    {
        _item.Fields.Add(field);
        return this;
    }

    /// <summary>
    /// Toggle `favorite` value on the in-flight Item.
    /// </summary>
    /// <returns></returns>
    public ItemBuilder ToggleFavorite()
    {
        _item.Favorite = !_item.Favorite;
        return this;
    }

    /// <summary>
    /// Performs final assembly of the new Item.
    /// </summary>
    /// <returns>The constructed <see cref="FullItem"/></returns>
    public FullItem Build() => _item;
}