using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityDarkSkin {
  internal static class Program {
    private const bool askForSystemVersion = true;
    private const bool askForUnityVersion = true;

    private static SkinType Skin;
    private static Arch SystemType;
    private static Version UnityVersion;

    private static string FilePath;
    private const string FileName = "Unity.exe";
    private const string FilePathBackup = FileName + ".BAK";

    private static int BytePosition;
    private static string[] Signatures;
    private static string[] InjectionCodes;

    private static void Main (string[] args) {
      Init();
      var path = "";
      if (args.Length > 0) path = args[0];
      ChooseFolder(path);
      Setup();
      Start();
    }

    private static void Init () {
      Console.Title = "Unity Dark Skin";
      SystemType = Arch.x64;
      if (askForSystemVersion) {
        Console.WriteLine("Choose system version:");
        Console.WriteLine("Unity.exe (64 bit): type '1'");
        Console.WriteLine("Unity.exe (32 bit): type '2'");
        Console.Write("\nYour answer: ");

        ConsoleKeyInfo key = Console.ReadKey();
        switch (key.KeyChar) {
          case '2':
            SystemType = Arch.x86;
            break;
          default:
            SystemType = Arch.x64;
            break;
        }
      }

      UnityVersion = Version.higherThan2018_2;
      if (askForUnityVersion) {
        Console.WriteLine("\n\nChoose unity version:");
        Console.WriteLine("2018.3 or more: type '1'");
        Console.WriteLine("2018.2 or less: type '2'");
        Console.Write("\nYour answer: ");

        ConsoleKeyInfo key = Console.ReadKey();
        switch (key.KeyChar) {
          case '2':
            UnityVersion = Version.lowerThan2018_2;
            break;
          default:
            UnityVersion = Version.higherThan2018_2;
            break;
        }
      }
    }

    private static void Setup () {
      if (UnityVersion == Version.higherThan2018_2) {
        if (SystemType == Arch.x64) {
          Signatures = new[] {
              "84C0750833C04883C4305BC38B034883C4305BC3",
              "84C0740833C04883C4305BC38B034883C4305BC3﻿"
          };

          InjectionCodes = new[] {
              "84C074",
              "84C075"
          };
        }
        else if (SystemType == Arch.x86) {
          // the following values are NOT THE GOOD ONE
          Signatures = new[] {
              "84C0750833C04883C4305BC38B034883C4305BC3",
              "84C0740833C04883C4305BC38B034883C4305BC3﻿"
          };

          InjectionCodes = new[] {
              "84C074",
              "84C075"
          };
        }
      }
      else {
        if (SystemType == Arch.x86) {
          // the following values are NOT THE GOOD ONE
          Signatures = new[] {
              "84C0750833C04883C4305BC38B034883C4305BC3",
              "84C0740833C04883C4305BC38B034883C4305BC3﻿"
          };

          InjectionCodes = new[] {
              "84C074",
              "84C075"
          };
        }
        else if (SystemType == Arch.x64) {
          // the following values are NOT THE GOOD ONE
          Signatures = new[] {
              "84C0750833C04883C4305BC38B034883C4305BC3",
              "84C0740833C04883C4305BC38B034883C4305BC3﻿"
          };

          InjectionCodes = new[] {
              "84C074",
              "84C075"
          };
        }
      }
    }

    private static void Start () {
      var Directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      FilePath = Directory + @"\" + FileName;
      Console.WriteLine("\nSearch unityExe: " + FilePath);

      if (File.Exists(FilePath)) {
        Console.WriteLine("--------");
        Console.WriteLine("Press any key to change skin...");
        Console.ReadKey();
        if (!File.Exists(FilePathBackup)) {
          Console.WriteLine("Unity.exe will be backed up as " + FilePathBackup);
          //File.Delete(FilePathBackup);
          File.Copy(FilePath, FilePathBackup);
        }
        else {
          Console.WriteLine("Unity.exe backup already exists");
        }

        BytePosition = GetBytePosition();

        try {
          if (BytePosition != 0) {
            Console.WriteLine("--------");

            GetSkinType(BytePosition);
            Console.WriteLine("Current skin: " + Skin);

            Console.WriteLine("Please wait...");
            ToggleSkinType();

            GetSkinType(BytePosition);
            Console.WriteLine("Current skin: " + Skin);
          }
          else {
            Console.WriteLine("--------");
            Console.WriteLine("Signature is not found. Choose another file.");
          }
        }
        catch (Exception e) {
          Console.WriteLine("--------");
          Console.WriteLine("Error has occured:" + e.Message);
          Console.WriteLine("Run application as an Administrator");
        }

        Console.WriteLine("--------");
        Console.WriteLine("Done!");
      }
      else {
        Console.WriteLine(FileName + " not found.\nPlease copy this application to folder with " + FileName);
      }

      Console.ReadKey();
    }

    private static void ChooseFolder (string unityExe = "") {
      Console.WriteLine("\n--------");
      Console.WriteLine("Is Unity.exe located in this file folder (y/n) ?");
      Console.WriteLine("To specify the unity.exe folder type 'n'");
      Console.Write("Your answer: ");

      ConsoleKeyInfo key = Console.ReadKey();
      string UnityFolder;
      switch (key.KeyChar) {
        case 'n':
          Console.Write("\nEnter the unity.exe file path: ");
          UnityFolder = Console.ReadLine();
          break;
        default:
          UnityFolder = ("" == unityExe) ? Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) : Path.GetDirectoryName(unityExe);
          break;
      }

      FilePath = UnityFolder + @"\" + FileName;
    }

    private static void ToggleSkinType () {
      switch (Skin) {
        case SkinType.Light:
          SwitchSkin(1);
          break;
        case SkinType.Dark:
          SwitchSkin(0);
          break;
      }
    }

    private static void SwitchSkin (int t) {
      byte[] value;
      switch (t) {
        case 0:
          using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(FilePath))) {
            binaryWriter.BaseStream.Position = BytePosition;
            //value = int.Parse(InjectionCodes[0], System.Globalization.NumberStyles.HexNumber);
            value = StringToByteArray(InjectionCodes[1]);
            binaryWriter.Write(value);
            binaryWriter.Flush();
            binaryWriter.Close();
          }

          Skin = SkinType.Light;
          break;
        case 1:
          using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(FilePath))) {
            binaryWriter.BaseStream.Position = BytePosition;
            //value = int.Parse(InjectionCodes[1], System.Globalization.NumberStyles.HexNumber);
            value = StringToByteArray(InjectionCodes[0]);
            binaryWriter.Write(value);
            binaryWriter.Flush();
            binaryWriter.Close();
          }

          Skin = SkinType.Dark;
          break;
      }
    }

    private static void GetSkinType (int offset) {
      using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(FilePath))) {
        binaryReader.BaseStream.Position = offset + 2;
        var skinFlag = binaryReader.ReadByte().ToString("X2");
        switch (skinFlag) {
          case "75":
            Skin = SkinType.Light;
            break;
          case "74":
            Skin = SkinType.Dark;
            break;
        }

        binaryReader.Close();
      }
    }

    // Returns offset of signature
    private static int GetBytePosition () {
      var position = 0;
      using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(FilePath))) {
        var FileBytes = new byte[binaryReader.BaseStream.Length];
        foreach (var Signature in Signatures) {
          var SignatureBytes = StringToByteArray(Signature);
          binaryReader.BaseStream.Position = 0L;

          binaryReader.Read(FileBytes, 0, FileBytes.Length);
          var pos = FindSignature(FileBytes, SignatureBytes);
          if (pos == -1) continue;
          position = pos;
          break;
        }

        binaryReader.Close();
      }

      return position;
    }

    // Searches in an executable file
    private static int FindSignature (IReadOnlyList<byte> bytes, IReadOnlyList<byte> search, int offset = 0) {
      var num = -1;
      if (bytes.Count <= 0 || search.Count <= 0 || (offset > bytes.Count - search.Count || bytes.Count < search.Count)) return num;
      for (var i = offset; i <= bytes.Count - search.Count; ++i) {
        if (bytes[i] != search[0]) continue;
        if (bytes.Count > 1) {
          var flag = true;
          for (var j = 1; j < search.Count; ++j) {
            if (bytes[i + j] == search[j]) continue;
            flag = false;
            break;
          }

          if (!flag) continue;
          num = i;
          break;
        }

        num = i;
        break;
      }

      return num;
    }

    // Hex string to array of bytes
    private static byte[] StringToByteArray (string hex) {
      return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
    }

    private enum Arch {
      x86 = 0,
      x64 = 1
    }

    private enum Version {
      lowerThan2018_2 = 0,
      higherThan2018_2 = 1
    }

    private enum SkinType {
      Dark = 0,
      Light = 1
    }
  }
}
