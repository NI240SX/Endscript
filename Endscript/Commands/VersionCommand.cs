using System;
using Endscript.Core;
using Endscript.Enums;
using Endscript.Exceptions;



namespace Endscript.Commands
{
	/// <summary>
	/// Command of type 'version [#.#.#.#]'.
	/// </summary>
	public class VersionCommand : BaseCommand
	{
		private System.Version _version;

		public override eCommandType Type => eCommandType.version;

		public override void Prepare(string[] splits)
		{
			if (splits.Length != 2 && splits.Length != 3) throw new InvalidArgsNumberException(splits.Length, 2, 3);

			this.CheckValidVersion(splits[1]);

			// add data in version to prevent Binercover-only scripts to be loaded by concurrent forks
			if (splits.Length == 3) if (splits[2].ToLower() != "binercover") throw new Exception($"Endscript version declares the extension {splits[2]} ; this fork only supports Binercover.");

			if (Version.Value.CompareTo(this._version) >= 0) return;
			else throw new Exception($"Endscript version {this._version} is higher than executable {Version.Value}");
		}

		public override void Execute(CollectionMap map)
		{
		}

		private void CheckValidVersion(string version)
		{
			try
			{

				this._version = new System.Version(version);

			}
			catch
			{

				throw new Exception($"Version stated is of invalid format");

			}
		}
	}
}
