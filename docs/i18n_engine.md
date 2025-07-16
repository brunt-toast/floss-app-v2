# Internationalization Engine 

As of commit `40dea006e1fe75faef2b1db1bb59ba24fb62829e`. 

This project uses a custom internationalization (hereafter i18n) engine. 

## Engine Usage

The engine is a service defined at [/src/FlossApp.Application/Services/I18n/I18nService.cs](../src/FlossApp.Application/Services/I18n/I18nService.cs). 

An instance of the engine requires an "anchor type". This is a type which must be sibling to a Resources directory. The default is `FlossApp.I18n.I18nLibTypeAnchor`, which is sibling to the directory [/src/FlossApp.I18n/Resources](../src/FlossApp.I18n/Resources). This is required to resolve the identifier for the resources that the language definitions become. 

The instance also requires a string "Language", which in actuality may represent any direct child directory of the Resources directory. The default is "en-US". 

The method `GetResources()` returns a `KeyValuePair<string,object>` which may be passed as `@attributes` on any HTML or blazor component. The `GetResource()` method will return a single instance of a string. Identifying a resource by name is covered further down in this document. 

## Resource Definition

Every resource must be declared in a JSON file matching the pattern `$RESOURCES/$LANG/**/*.json`, where `$RESOURCES` represents a directory named `Resources` which is sibling to an anchor type, and `$LANG` represents a folder whose name SHOULD represent a language code according to [IETF BCP 47](https://www.rfc-editor.org/info/bcp47).

The JSON file must be serializable as a `Dictionary<string,object>`. Values SHOULD be limited to simple types. Behaviour in the case of values that are complex JSON objects is not defined. The base name of the file SHOULD be unique within the `$LANG` folder. Behaviour of resolution in the case of multiple files with the same base name is undefined. 

The JSON files are loaded as embedded resources at compile time. This means that new files must be marked as embedded resources. For all files under [/src/FlossApp.I18n/Resources/](../src/FlossApp.I18n/Resources/), this is done automatically by the following segment of [/src/FlossApp.I18n/FlossApp.I18n.csproj](../src/FlossApp.I18n/FlossApp.I18n.csproj):

```xml
  <ItemGroup>
    <EmbeddedResource Include="Resources\**\*" />
  </ItemGroup>
```

## Resource Resolution

A resource can be referenced as `$FILE.$KEY`, where `$FILE` is the basename of a JSON file with no `.json` extension and `$KEY` is a key within that file. 

For the `GetResource` method, a single string value will be returned. This will be found in a file whose basename is `$FILE.json`. The value will be that of the expression with the key `$KEY` will be resolved. 

For the `GetResources` method, the method will pull all key-value pairs from `$FILE` where the JSON key matches `$KEY.*`. The returned keys will not include the `$KEY.` prefix. 

```json
{
    "group.a": 1,
    "group.b": 2
}
// resolves as 
{
    "a": 1,
    "b": 2
}
```

If a resource cannot be found on the first pass, the engine will treat the name of the class that requested the resource as an implicit value for `$FILE`. 

```csharp
class MyClass
{
    void M() 
    {
        string s = _i18nService.GetResource("segment1.segment2");
        // first, looks for key "segment2" in some file "segment1.json"
        // then, looks for "segment1.segment2" in some file "MyClass.json"
        // finally, returns String.Empty 
    }
}
```

All resource resolution is case-insensitive. 

## Service Tests

The internationalization service is tested at [/test/FlossApp.Application.Tests/Services/I18n/I18nServiceTests.cs](../test/FlossApp.Application.Tests/Services/I18n/I18nServiceTests.cs). 

## Integrity Tests

Integrity tests, located at [/test/FlossApp.I18n.Tests/IntegrityTests.cs](../test/FlossApp.I18n.Tests/IntegrityTests.cs), ensure the following conditions: 
* Each `$LANG` folder contains an identical set of `JSON` files as the default language's folder
* Each `$LANG/**/*.json` file contains the same keys as its `$DEFAULTLANG/**/*.json` counterpart. 