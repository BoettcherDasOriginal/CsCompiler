# CsCompiler
 simple c# compiler written in c#

## Compiling code

To compile your source code, you need to create a CCMK file.
The CCMK file tells the compiler everything it needs to know in order to compile your source code.

### CCMK file

The CCMK file is in yaml format and looks something like this:

```
# YAML ccmk example

# Add Referenced Assemblies
ReferencedAssemblies:
- DEBUG.ccmk

# Source Files
SrcFiles: 
- SrcExample1.cs
- SrcExample2.cs

# Specify the assembly file name to generate
OutputAssembly: ExampleFile.exe
```

- In the Referenced Assemblies list you can add your own required assemblies that are not included in the standard .NET Framework.
- In the Source Files list you can specify the path to your individual C # source files that you want to compile.
- The output assembly determines the fully compiled file, which must end with .exe or .dll.

### What now?

Now you just have to open the CCMK file with the CsCompiler and wait until it is finished
