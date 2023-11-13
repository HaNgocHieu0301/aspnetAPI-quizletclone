using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class FolderDAO
{
    public static List<Folder> Get()
    {
        try
        {
            using var context = new ServicesFolderContext();
            return context.Folders.AsNoTracking().ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static Folder Get(int folderId)
    {
        try
        {
            using var context = new ServicesFolderContext();
            return context.Folders.AsNoTracking().FirstOrDefault(o => o.FolderId == folderId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static int Add(Folder folder)
    {
        try
        {
            using var context = new ServicesFolderContext();
            context.Folders.Add(folder);
             context.SaveChanges();
             return folder.FolderId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static int Update(Folder folder)
    {
        try
        {
            using var context = new ServicesFolderContext();
            context.Folders.Update(folder);
            return context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static void Remove(int folderId)
    {
        try
        {
            using var context = new ServicesFolderContext();
            var folder = context.Folders.AsNoTracking().FirstOrDefault(o => o.FolderId == folderId);
            if(folder == null) return;
            context.Folders.Remove(folder);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}