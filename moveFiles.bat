@echo off
mkdir %~2
copy %~1\Azure.Core.1.21.0\lib\netstandard2.0\Azure.Core.dll %~2
copy %~1\Azure.Messaging.WebPubSub.1.0.0\lib\netstandard2.0\Azure.Messaging.WebPubSub.dll %~2
copy %~1\Microsoft.Bcl.AsyncInterfaces.1.0.0\lib\netstandard2.0\Microsoft.Bcl.AsyncInterfaces.dll %~2
copy %~1\System.Buffers.4.5.1\lib\netstandard2.0\System.Buffers.dll %~2
copy %~1\System.Diagnostics.DiagnosticSource.4.6.0\lib\netstandard1.3\System.Diagnostics.DiagnosticSource.dll %~2
copy %~1\System.Memory.4.5.4\lib\netstandard2.0\System.Memory.dll %~2
copy %~1\System.Memory.Data.1.0.2\lib\netstandard2.0\System.Memory.Data.dll %~2
copy %~1\System.Runtime.CompilerServices.Unsafe.4.6.0\lib\netstandard2.0\System.Runtime.CompilerServices.Unsafe.dll %~2
copy %~1\System.Text.Encodings.Web.4.7.2\lib\netstandard2.0\System.Text.Encodings.Web.dll %~2
copy %~1\System.Text.Json.4.6.0\lib\netstandard2.0\System.Text.Json.dll %~2
copy %~1\System.Threading.Tasks.Extensions.4.5.4\lib\netstandard2.0\System.Threading.Tasks.Extensions.dll %~2