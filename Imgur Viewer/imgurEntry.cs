using System;
using System.Xml.Linq;

public class imgurEntry
{
    public string Hash { get; private set; }
    public string Title { get; private set; }
    public string Ext { get; private set; }

    public imgurEntry(string hash, string title, string ext)
	{
        this.Hash = hash;
        this.Title = title;
        this.Ext = ext;

	}  
}
