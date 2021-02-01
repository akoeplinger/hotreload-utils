using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Diffy
{

    public class Config
    {

        public static ConfigBuilder Builder () => new ConfigBuilder ();

        public class ConfigBuilder {
            internal ConfigBuilder () {}

            public bool Live {get; set;} = false;

            public List<KeyValuePair<string,string>> Properties {get; set;} = new List<KeyValuePair<string,string>> ();
            public string ProjectPath {get; set; } = "";

            public string ScriptPath {get; set; } = "";
            public Config Bake () {
                return new MsbuildConfig(this);
            }
        }

        protected Config (ConfigBuilder builder) {
            Live = builder.Live;
            Properties = builder.Properties;
            ProjectPath = builder.ProjectPath;
            ScriptPath = builder.ScriptPath;
        }

        public bool Live { get; }

        /// Additional properties for msbuild
        public IReadOnlyList<KeyValuePair<string,string>> Properties { get; }


        /// the csproj for this project
        public string ProjectPath { get; }

        /// the files to watch for live changes
        public string LiveCodingWatchPattern { get => "*.cs"; }

        /// the directory to watch for live changes
        public string LiveCodingWatchDir { get => Path.GetDirectoryName(ProjectPath) ?? "."; }

        public string ScriptPath { get; }
    }

    internal class MsbuildConfig : Config {
        internal MsbuildConfig (ConfigBuilder builder) : base (builder) {}
    }
}