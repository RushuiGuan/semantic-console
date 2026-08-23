using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Commands;

[assembly: Verb<ReadConfirmationParams, ReadConfirmation>(ReadConfirmationParams.Verb, Description = ReadConfirmationParams.Description)]
[assembly: Verb<ReadContentParams, ReadContent>(ReadContentParams.Verb, Description = ReadContentParams.Description)]
[assembly: Verb<ReadSecretParams, ReadSecret>(ReadSecretParams.Verb, Description = ReadSecretParams.Description)]
[assembly: Verb<ReadSelectParams, ReadSelect>(ReadSelectParams.Verb, Description = ReadSelectParams.Description)]
[assembly: Verb<ReadTextParams, ReadText>(ReadTextParams.Verb, Description = ReadTextParams.Description)]
[assembly: Verb<ReadNumberParams, ReadNumber>(ReadNumberParams.Verb, Description = ReadNumberParams.Description)]
[assembly: Verb<ReadIntParams, ReadInt>(ReadIntParams.Verb, Description = ReadIntParams.Description)]
[assembly: Verb<ReadUrlParams, ReadUrl>(ReadUrlParams.Verb, Description = ReadUrlParams.Description)]
[assembly: Verb<WriteActionParams, WriteAction>(WriteActionParams.Verb, Description = WriteActionParams.Description)]
[assembly: Verb<WriteContentParams, WriteContent>(WriteContentParams.Verb, Description = WriteContentParams.Description)]
[assembly: Verb<WriteFeedbackParams, WriteFeedback>(WriteFeedbackParams.Verb, Description = WriteFeedbackParams.Description)]
[assembly: Verb<WriteInfoParams, WriteInfo>(WriteInfoParams.Verb, Description = WriteInfoParams.Description)]
