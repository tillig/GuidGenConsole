# GuidGenConsole
Command-line utility for generating GUID values, formatting them, and putting them on the clipboard.

## Options

| Option | Description |
| ------ | ----------- |
| `-f`, `--format` | The GUID format to generate. If omitted, the GUID will be unformatted. |
| `-s` | If 'format' is 'custom,' this is the custom format string. Use String.Format style (`{0:NDBPXUL}`) - U/L indicates upper/lower case. |
| `-q`, `--quantity` | The number of GUIDs to generate. |

## Formats

The `-f` or `--format` can be one of these values:

| Value | Description | Example |
| ----- | ----------- | ------- |
| `ole` | IMPLEMENT_OLECREATE(...) | `// {C3731F56-D23B-4CDD-9AB6-BED470842A6B}`<br />`IMPLEMENT_OLECREATE(<<class>>, <<external_name>>,`<br />`0xc3731f56, 0xd23b, 0x4cdd, 0x9a, 0xb6, 0xbe, 0xd4, 0x70, 0x84, 0x2a, 0x6b);` |
| `def` | DEFINE_GUID(...) | `// {C3731F56-D23B-4CDD-9AB6-BED470842A6B}`<br />`DEFINE_GUID(<<name>>,`<br />`0xc3731f56, 0xd23b, 0x4cdd, 0x9a, 0xb6, 0xbe, 0xd4, 0x70, 0x84, 0x2a, 0x6b);` |
| `struct` | static const struct Guid = {...} | `// {C3731F56-D23B-4CDD-9AB6-BED470842A6B}`<br />`static const GUID <<name>> =`<br />`{ 0xc3731f56, 0xd23b, 0x4cdd, { 0x9a, 0xb6, 0xbe, 0xd4, 0x70, 0x84, 0x2a, 0x6b } };` |
| `reg` | Registry format {xxxxxxxx-xxxx-...} | `{d2daab3e-c8ed-4985-b7f3-7209e4bc15fe}` |
| `custom` | Custom format (specify `-s` with the format) | |

If you specify `-f custom` or otherwise omit the `-f` parameter, you can use `String.Format` style to indicate a custom format.

| Parameter | Example |
| --------- | ------- |
| `-s {0:N}` | `16ddb3ee514449069e1d10168759d841` |
| `-s {0:NU}` | `16DDB3EE514449069E1D10168759D841` |

## Examples

| Command | Yields |
| ------- | ------ |
| `guidgenconsole -q 2` | `5c29f447-34c3-4eda-9000-1a4e4937d5a1`<br />`3a7ab022-51c6-43e9-98df-ffd3e75927be` |
| `guidgenconsole -s {0:NU}` | `019C47CAACC742EB93EAFEC8C9F27F5F` |
| `guidgenconsole -f reg -q 3` | `{a13a49c3-250d-4400-a7d5-be6df686a241}`<br />`{d876bf20-55ae-49d2-a924-660d19be7e33}`<br />`{3cc978d3-a32a-480f-bb41-42904e9492ab}` |