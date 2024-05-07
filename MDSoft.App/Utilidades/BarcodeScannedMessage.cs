using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tracking.Utilidades
{
    public class BarcodeScannedMessage : ValueChangedMessage<BarcodeResult>
    {
        public BarcodeScannedMessage(BarcodeResult value) : base(value)
        {
        }
    }
}
