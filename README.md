# MKV to MP4 Converter

A Windows command line application to convert MKV files to MP4 files. The application loops through directories of MKV files and calls VideoProc Converter to convert the MKV files to MP4 files. The MP4 files are set with video properties to make them nicer to use. Converted files are recorded in Microsoft Access to provide a database of files available.

## Getting Started

### Prerequisites

#### VideoProc Converter

The application uses [VideoProc Converter](https://www.videoproc.com/video-converting-software/) from Digiarty Software to do the actual video conversion. As a side note, I tried several different converters both free and paid, and I found VideoProc Converter to be superior to the others. The application uses Mouse and Keyboard input to control VideoProc Converter. This makes the application sensitive to any UI changes in VideoProc Converter. The application is currently designed for VideoProc Converter 4.8.

#### Microsoft Access

The application uses Microsoft Access to record information about each file converted. Edit the Database.cs file to point the application at the data store you wish to use. The database tables look like the following:

<img src="Database Diagram.png" width="50%">

A Microsoft Access database with an empty main table (tblVideoInfo) is present in the MKVToMP4Converter.accdb file. The supporting code tables have records in them to make getting started easier.

### Preparing the Application

#### Set Output Directory

Update the Program.cs outputDirectory to be correct for your needs.

#### Set Database location

Update the Database.cs connectionString to point to your database.

#### Build application

Load the MKVToMP4Converter.sln solution in Visual Studio and Build the solution.

## Using the Application

### Preparing Video Information Files

Each MKV file to be converted should be placed in its own directory. Along with the MKV file there should be two other files in the directory. The first is a jpg file with the cover art to be associated with the video. The second is a file called `VideoInfo.txt` that supplies information to the application about the video.

If you are converting multiple MKV files, you should create the directories for the files under one common directory. You will then specify the common directory when running the application.

The `VideoInfo.txt` file has the following format:

```json
{
    "Title": "Cool Movie",
    "MKVFile": "Cool Movie.mkv",
    "CoverFile": "Cool Movie.jpg",
    "OutputFile": "Cool Movie.mp4",
    "Year": "2002",
    "Rating": "PG",
    "Duration": "02:05:16",
    "Quality": "Blu-ray"
}
```

The fields have the following meanings:

Title - The title of the video. It will be stored as the title in the MP4.

MKVFile - The name of the source MKV file. This is the file to be converted.

CoverFile - The name of the cover art jpg file. The cover art will be included in the MP4.

OutputFile - The name of the output MP4 file. It will be stored in the output directory you set.

Year - The year of the video. It will be stored as the year in the MP4 and used as the created/modified dates of the MP4.

Rating - The MPAA rating of the video. Values used must exist in the tblRating table.

Duration - The length of the video. This value must match the actual length of the video. The application checks the length of the output video to confirm that the entire video was converted. Windows Explorer file property details shows the length of the video.

Quality - The quality of the video. Values used must exist in the tblQuality table. Currently `DVD` and `Blu-ray` are the possible values.

### Running the Application

Change directory to the location of your MKVToMP4Converter.exe file.

Run the application with a command like:

```
MKVToMP4Converter.exe video-directory-location
```

Substitute the location of the directory above your MKV directories for `video-directory-location`.

While running, the application will move the mouse and enter keyboard input. It is best to leave the computer alone until the application is finished to not confuse things with multiple sources of mouse and keyboard input. The application expects VideoProc Converter to be the top window and be centered on a 4K display for the mouse click locations to be accurate.

A run of the application looks like this:

```
C:\Utils\MKVToMP4Converter\bin\Debug\net6.0>MKVToMP4Converter.exe C:\Video
2023-01-01 08:57:55: MKVToMP4Converter started
2023-01-01 08:57:55: Converting "C:\Video\Test Video 1" started
2023-01-01 09:38:49: Converting "C:\Video\Test Video 1" finished
2023-01-01 09:38:49: Converting "C:\Video\Test Video 2" started
2023-01-01 10:08:52: Converting "C:\Video\Test Video 2" finished
2023-01-01 10:08:52: Converting "C:\Video\Test Video 3" started
2023-01-01 10:42:16: Converting "C:\Video\Test Video 3" finished
2023-01-01 10:42:16: MKVToMP4Converter finished

C:\Utils\MKVToMP4Converter\bin\Debug\net6.0>
```

## Notes

The created/modified dates of the MP4 are set to help with ordering of videos. I found that some video players use these dates for ordering when presenting a list of videos.

VideoProc Converter sometimes does not save the cover art properly in the MP4 file and no cover art is shown. I reported this as a bug to them. When this happens, I use [Mp3tag](https://www.mp3tag.de/en/) to add the cover art manually. This changes the modified date, so I use [BulkFileChanger](http://www.nirsoft.net/utils/bulk_file_changer.html) to change the date back to its former value.
