using Albatross.CommandLine.Annotations;
using Albatross.SemanticConsole.Commands;

[assembly: Verb<ReadConfirmationParams, ReadConfirmation>("read-confirm", Description = ReadConfirmationParams.Description)]
[assembly: Verb<ReadContentParams, ReadContent>("read-content", Description = ReadContentParams.Description)]
[assembly: Verb<ReadSecretParams, ReadSecret>("read-secret", Description = ReadSecretParams.Description)]
[assembly: Verb<ReadSelectParams, ReadSelect>("read-select", Description = ReadSelectParams.Description)]
[assembly: Verb<ReadTextParams, ReadText>("read-text", Description = ReadTextParams.Description)]
[assembly: Verb<ReadNumberParams, ReadNumber>("read-number", Description = ReadNumberParams.Description)]
[assembly: Verb<ReadIntParams, ReadInt>("read-int", Description = ReadIntParams.Description)]
[assembly: Verb<ReadUrlParams, ReadUrl>("read-url", Description = ReadUrlParams.Description)]
[assembly: Verb<WriteActionParams, WriteAction>("write-action", Description = WriteActionParams.Description)]
[assembly: Verb<WriteContentParams, WriteContent>("write-content", Description = WriteContentParams.Description)]
[assembly: Verb<WriteFeedbackParams, WriteFeedback>("write-feedback", Description = WriteFeedbackParams.Description)]
[assembly: Verb<WriteInfoParams, WriteInfo>("write-info", Description = WriteInfoParams.Description)]
