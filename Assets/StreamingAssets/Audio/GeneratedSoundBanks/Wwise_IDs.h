/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ENEMYDRONEAMB = 1504246845U;
        static const AkUniqueID HUBMUSICSTART_EVENT = 2461714704U;
        static const AkUniqueID LAZERFIRE_EVENT = 3822340686U;
        static const AkUniqueID LEVELSTART_EVENT = 1666178350U;
        static const AkUniqueID OBJECTIVECOMPLETE_EVENT = 551763010U;
        static const AkUniqueID PLAYERDISCOVERED_EVENT = 2538572501U;
        static const AkUniqueID TARGETHOOKED = 2827905996U;
        static const AkUniqueID TARGETSTASHED = 2045263066U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace PLAYERDETECTIONSTATE
        {
            static const AkUniqueID GROUP = 3924600286U;

            namespace STATE
            {
                static const AkUniqueID DISCOVERED = 2206681103U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STEALTH = 2909291642U;
            } // namespace STATE
        } // namespace PLAYERDETECTIONSTATE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MAINAUDIOVOLUME = 3830172926U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAINLEVELSOUNDBANK = 4096625075U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
