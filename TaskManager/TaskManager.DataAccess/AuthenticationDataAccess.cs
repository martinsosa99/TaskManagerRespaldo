using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using TaskManager.IDataAccess;

namespace TaskManager.DataAccess;

public class AuthenticationDataAccess : IAuthenticationDataAccess
{

    private DataBaseContext DataBaseContext { get; set; }

    public AuthenticationDataAccess(DataBaseContext dataBaseContext)
    {
        this.DataBaseContext = dataBaseContext;
    }

    public Session Login(User userWhoWantsToLogin, string token)
    {
        try
        {
            this.Logout(userWhoWantsToLogin);
            this.DataBaseContext.SaveChanges();

        }
        catch (Exception ex)
        {

        }

        Session session = new Session();
        session.User = userWhoWantsToLogin;
        session.Token = token;

        this.DataBaseContext.Set<User>().Attach(session.User);
        this.DataBaseContext.Set<Session>().Add(session);

        this.DataBaseContext.SaveChanges();

        return session;
    }

    public void Logout(string usersToken)
    {
        Session sessionWeWantToDelete = this.Sessions().FirstOrDefault(session => session.Token == usersToken);

        this.DataBaseContext.Set<Session>().Remove(sessionWeWantToDelete);

        this.DataBaseContext.SaveChanges();
    }

    public List<Session> Sessions()
    {
        return this.DataBaseContext.Set<Session>().Include(session => session.User).ToList<Session>();
    }

    public bool UserIsLogged(User user)
    {
        List<Session> systemSessions = this.Sessions();

        bool userIsLogged = false;

        Session systemSession = systemSessions.FirstOrDefault(currentSession => currentSession.User.Equals(user));

        if (systemSession != null)
            userIsLogged = true;

        return userIsLogged;
    }

    public bool UserIsLogged(string usersToken)
    {
        List<Session> systemSessions = this.Sessions();

        bool userIsLogged = false;

        Session systemSession = systemSessions.FirstOrDefault(currentSession => currentSession.Token == usersToken);

        if (systemSession != null)
            userIsLogged = true;

        return userIsLogged;
    }

    public User GetUserByToken(string usersToken)
    {
        List<Session> systemSessions = this.Sessions();

        User userToReturn = null;

        for (int index = 0; index < systemSessions.Count; index++)
        {
            Session systemSession = systemSessions[index];

            if (systemSession.Token == usersToken)
            {
                userToReturn = systemSession.User;
                break;
            }
        }

        return userToReturn;
    }

    private void Logout(User userWhoWantsToLogin)
    {
        Session sessionWeWantToDelete = this.Sessions().FirstOrDefault(session => session.User.Equals(userWhoWantsToLogin));

        this.DataBaseContext.Set<Session>().Remove(sessionWeWantToDelete);

        this.DataBaseContext.SaveChanges();
    }

}