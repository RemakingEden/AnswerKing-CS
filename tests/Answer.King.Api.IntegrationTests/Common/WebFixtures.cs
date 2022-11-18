﻿using Alba;

namespace Answer.King.Api.IntegrationTests.Common;

public class WebFixtures : IAsyncLifetime
{
    public IAlbaHost AlbaHost = null!;

    private static readonly string TestDbName = $"Answer.King.{Guid.NewGuid()}.db";

    public async Task InitializeAsync()
    {
        this.AlbaHost = await Alba.AlbaHost.For<Program>(hostBuilder =>
        {
            hostBuilder.UseSetting("ConnectionStrings:AnswerKing", $"filename={TestDbName};Connection=Shared;");
        });
    }

    public async Task DisposeAsync()
    {
        await this.AlbaHost.DisposeAsync();
        File.Delete($".\\{TestDbName}");
    }
}