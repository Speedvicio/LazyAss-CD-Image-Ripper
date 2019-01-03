# LazyAss-CD-Image-Ripper
Small tool to dump/convert/ripping physical CD or virtual images

This tool is for the ripping of console games, specifically PC-CD, PC-FX, SATURN and in any cases you can convert PSX games.
The same tool should also work with games for SEGA CD and other era cd games, but has not been tested personally.

Final extracted game can be started via Mednafen for example.

# Screenshot

<a href="https://a.fsdn.com/con/app/proj/lazyass-cd-image-ripper/screenshots/LazyAss.png/1"><img src="https://a.fsdn.com/con/app/proj/lazyass-cd-image-ripper/screenshots/LazyAss.png/1" width="200" /></a>

**Small how to:**

To refresh or Eject/Unmount a Cd/Dvd unit, press right mouse button on Cd/Dvd combo list

* Convert from a cue/bin
1) Select a cue file by cue/bin button<br>
-- The tool try to detect the cue type of file from MODE1/2352, MODE2/2352 and MODE1/2048.<br>
-- If the value is incorrect, select it from the drop down menu (CD Image Options) the type of the source or destination file.
2) Set a name for ripped game (no empty values are allowed)
3) Select a encode format for extracted audio file
4) Select the parameters for final audio encode
5) Select the destination path of file extracted and encoded (no empty values are allowed)
6) Trim wave flag try to reduce the size of extracted wave file (this task is optional and don't recommended) 
7) Press start conversion button and wait for the end of task

* Dump/Convert from a other virtual cd image or from a physical or virtual cd/dvd drives
1) Check Drive Letter option (the tool detect all available cd/dvd device and the cue/bin button change to daemon tool icon)<br>
2) Mount a virtual cd/dvd image file by your favourite virtual image tool.<br>
-- If Daemon Tool is installed on you pc you can auto mount a virtual image on the fly.<br>
-- The tool try to auto detect the version of Daemon Tool installed from lite,pro and ultra or you can select it manually.
3) Set a name for ripped game, the tool try to detect the label name from cd/dvd (no empty values are allowed)
4) Select the destination path of file extracted and encoded (no empty values are allowed).

**<< To only dump >>**<br>
5) Check Dump Only option, all other audio section will be disabled<br>
-- If your cd/dvd image or physical support is damaged you can try to check Paranoia option.<br>
-- This option perform overlapped reading to avoid jitter with  additional  checks  of the read audio data<br>

**<< To dump and convert >>**<br>
5) Select a encode format for extracted audio file and select the parameters for final audio encode.<br>
6) Press start conversion button and wait for the end of task (the operation may take several minutes)<br>

* Rebuild a converted cue/iso/"wave" or lossy codec into cue/iso/"wav", "bin" and/or ccd/img/sub format
1) Select a cue (ripped) file by cue/bin button<br>
2) Set a name for ripped game (no empty values are allowed)<br>
3) Select the destination path of file extracted and encoded (no empty values are allowed)<br>
4) Check Rebuild ripped CUE option<br>
7) Press start conversion button and wait for the end of task<br>
-- In addiction you can also create a Clone CD file (ccd/img/sub).<br>

Note: You can also Dump and convert a audio cd but this operation take several minutes

### Requirements
* Microsoft .NET Framework 4.6.1 or upper

### LazyAss in addiction use this external exe/library:
* [bchunk.exe](http://he.fi/bchunk/)
* [bin2iso.exe](http://users.eastlink.ca/~doiron/bin2iso/)
* [cdrdao.exeand toc2cue.exe](http://cdrdao.sourceforge.net/)
* [cue2toc.exe](http://cue2toc.sourceforge.net/)
* [SoX](http://sox.sourceforge.net/)
* [BizHawk.Common.dll and BizHawk.Emulation.DiscSystem.dll](https://github.com/TASVideos/BizHawk/tree/master/BizHawk.Client.DiscoHawk)
* [LinqBridge.dll](https://bitbucket.org/raboof/linqbridge)
* [DiscTools.dll](https://github.com/Asnivor/DiscTools)
* [zlib1.dll](https://www.zlib.net/)
* [shntool.exe](http://shnutils.freeshell.org/)
