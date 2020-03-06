using AIS.Parser;

public class Talker
{
    public Talkers Value { get; }

    internal Talker(TalkerId talkerId)
    {
        switch (talkerId)
        {
            case TalkerId.AB: Value = Talkers.BaseAIS; break;
            case TalkerId.AD: Value = Talkers.DependentAISBase; break;
            case TalkerId.AI: Value = Talkers.MobileAIS; break;
            case TalkerId.AN: Value = Talkers.AidToNavigation; break;
            case TalkerId.AR: Value = Talkers.Receiving; break;
            case TalkerId.AS: Value = Talkers.LimitedBase; break;
            case TalkerId.AT: Value = Talkers.Transmitting; break;
            case TalkerId.AX: Value = Talkers.Repeater; break;
            case TalkerId.BS: Value = Talkers.Deprecated; break;
            case TalkerId.SA: Value = Talkers.PhysicalStore; break;
            default: Value = Talkers.Undefined; break;
        }
    }
}