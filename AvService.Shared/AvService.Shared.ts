//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

export interface IScanHubClient
{
	SendStartScanOnDemandNotification(notification: StartScanOnDemandNotification) : void;
	SendStopScanOnDemandNotification(notification: StopScanOnDemandNotification) : void;
	SendStopScanSuccessNotification(notification: StopScanSuccessNotification) : void;
	SendThreatFoundNotification(notification: ThreatFoundNotification) : void;
	SendScanInProgressNotification(notification: ScanInProgressNotification) : void;
	DisconnectClient() : void;
}
export interface IScanHubServer
{
	Connect() : void;
	Disconnect() : void;
	DisableRealTimeScan() : void;
	EnableRealTimeScan() : void;
	StartOnDemandScan() : void;
	StopOnDemandScan() : void;
	PublishUnsentNotifications() : void;
}
export class ConnectionDeniedNotification extends Notification
{
}
export class InfectedObject
{
	public FilePath: string;
	public ThreatName: string;
}
export class Notification
{
	public NotificationTime: any;
}
export class ScanInProgressNotification extends Notification
{
}
export class StartScanOnDemandNotification extends Notification
{
}
export class StopScanNotification extends Notification
{
	public Reason: string;
}
export class StopScanOnDemandNotification extends StopScanNotification
{
	public Reason: string;
}
export class StopScanSuccessNotification extends StopScanNotification
{
	public Reason: string;
}
export class ThreatFoundNotification extends Notification
{
	public InfectedObjects: InfectedObject[];
}
