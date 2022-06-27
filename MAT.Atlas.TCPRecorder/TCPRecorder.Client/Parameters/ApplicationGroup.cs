using TCPRecorder.Client.Packet;

namespace TCPRecorder.Client.Parameters
{
    public sealed class ApplicationGroup : FieldDetails, IApplicationGroup
    {
        public ApplicationGroup(string name, string description = null)
            : base(name, description)
        {
        }
    }
}