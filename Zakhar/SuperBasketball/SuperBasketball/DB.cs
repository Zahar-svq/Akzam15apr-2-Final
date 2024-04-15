using System;
using System.Collections.Generic;
using MySqlConnector;
using SuperBasketball.Models;

namespace SuperBasketball;

public class DB
{
    private MySqlConnectionStringBuilder _stringBuilder = new MySqlConnectionStringBuilder()
    {
        Server = "10.10.0.24",
        Port = 3306,
        UserID = "user01pro",
        Password = "user_01",
        Database = "exam_db"
    };
    
    public Employee CheckEmployee(string login, string password)
    {
        string sql = "select * from employee where employee.name = @login limit 1";
        MySqlConnection sqlConnection = new MySqlConnection(_stringBuilder.ConnectionString);
        MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);
        sqlCommand.Parameters.AddWithValue("@login", login);

        Employee employee = new Employee();
        try
        {
            sqlConnection.Open();
            MySqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                employee = new Employee()
                {
                    Id = reader.GetInt32("id"),
                    Login = reader.GetString("login"),
                    Password = reader.GetString("password"),
                    Role = reader.GetString("role")
                };

                if (employee.Password != password)
                {
                    throw new Exception();
                }
            }
            return employee;

        }
        catch (Exception e)
        {
            employee = new Employee();
            return employee;
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    public List<Player> GetPlayers(string search, int filter_id)
    {
        string sql = "select * from player join team on player.team_id = team.id join position on player.position_id = position.id";
        MySqlConnection sqlConnection = new MySqlConnection(_stringBuilder.ConnectionString);
        MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);

        List<Player> players = new List<Player>();
        try
        {
            sqlConnection.Open();
            MySqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                Player player = new Player()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Birthday = reader.GetDateTimeOffset("birthday"),
                    DateStart = reader.GetDateTimeOffset("date_start"),
                    Height = reader.GetDouble("height"),
                    Mass = reader.GetDouble("mass"),
                    Position = reader.GetString("position.name"),
                    PositionId = reader.GetInt32("position.id"),
                    Team = reader.GetString("team.name"),
                    TeamId = reader.GetInt32("team.id"),
                };
                players.Add(player);
            }

            return players;

        }
        catch (Exception e)
        {
            players = new List<Player>();
            return players;
        }
        finally
        {
            sqlConnection.Close();
        }
    }

    public List<Position> GetPositions()
    {
        string sql = "select * from position";
        MySqlConnection sqlConnection = new MySqlConnection(_stringBuilder.ConnectionString);
        MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);

        List<Position> positions = new List<Position>();
        try
        {
            sqlConnection.Open();
            MySqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                Position position = new Position()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name")
                };
                positions.Add(position);
            }

            return positions;

        }
        catch (Exception e)
        {
            positions = new List<Position>();
            return positions;
        }
        finally
        {
            sqlConnection.Close();
        }
    }
    
    public List<Team> GetTeams()
    {
        string sql = "select * from team";
        MySqlConnection sqlConnection = new MySqlConnection(_stringBuilder.ConnectionString);
        MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);

        List<Team> teams = new List<Team>();
        try
        {
            sqlConnection.Open();
            MySqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.HasRows && reader.Read())
            {
                Team team = new Team()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name")
                };
                teams.Add(team);
            }

            return teams;

        }
        catch (Exception e)
        {
            teams = new List<Team>();
            return teams;
        }
        finally
        {
            sqlConnection.Close();
        }    
    }

    public bool DeletePlayer(int id)
    {
        return false;
    }

    public bool AddPlayer(Player player)
    {
        return false;
    }
}