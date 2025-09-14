using System;
using CLI.UI.Core;
using RepositoryContracts;

namespace CLI.UI.Views;

public class UserListView : IView
{
    public required IUserRepository UserRepository { private get; init; }

    public Task RenderAsync()
    {
        var users = UserRepository.GetManyAsync();
        var list = users.Select(u => new[] { u.Id.ToString(), u.Name, u.Password }).ToList();

        int rows = list.Count;
        int cols = rows > 0 ? list[0].Length : 3;
        var rect = new string[rows + 1, cols];
        rect[0, 0] = " ◆ Id ◆ ";
        rect[0, 1] = " ◆ Name ◆ ";
        rect[0, 2] = " ◆ Password ◆ ";
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                rect[i + 1, j] = list[i][j];

        Utils.DrawTable(rect);

        return Task.CompletedTask;
    }
}
