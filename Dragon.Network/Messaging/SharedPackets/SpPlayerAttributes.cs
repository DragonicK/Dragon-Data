﻿namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerAttributes : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerAttributes;
    public int Points { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Spirit { get; set; }
    public int Will { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Accuracy { get; set; }
    public int Evasion { get; set; }
    public int Parry { get; set; }
    public int Block { get; set; }
    public int MagicAttack { get; set; }
    public int MagicDefense { get; set; }
    public int MagicAccuracy { get; set; }
    public int MagicResist { get; set; }
    public int Concentration { get; set; }
    public int CritRate { get; set; }
    public int CritDamage { get; set; }
    public int ResistCritRate { get; set; }
    public int ResistCritDamage { get; set; }
    public int DamageSuppression { get; set; }
    public int HealingPower { get; set; }
    public int FinalDamage { get; set; }
    public int Amplification { get; set; }
    public int Enmity { get; set; }
    public int AttackSpeed { get; set; }
    public int CastSpeed { get; set; }
    public int ResistSilence { get; set; }
    public int ResistBlind { get; set; }
    public int ResistStun { get; set; }
    public int ResistStumble { get; set; }
    public int PveAttack { get; set; }
    public int PveDefense { get; set; }
    public int PvpAttack { get; set; }
    public int PvpDefense { get; set; }
    public int FireAttack { get; set; }
    public int FireDefense { get; set; }
    public int WaterAttack { get; set; }
    public int WaterDefense { get; set; }
    public int EarthAttack { get; set; }
    public int EarthDefense { get; set; }
    public int WindAttack { get; set; }
    public int WindDefense { get; set; }
    public int LightAttack { get; set; }
    public int LightDefense { get; set; }
    public int DarkAttack { get; set; }
    public int DarkDefense { get; set; }
}