# Events Overview

## Steps to use

#### Create Event from predefined types

1. Create -> Game Events -> Select Data Type Needed
   - Forsake of explanation call it `Event_new` of `int` type
2. In the script that needs to call it, create a serialized field of `IntEventSO` and attach `Event_new` from inspector. Call Raise on event along with an `int` argument.
3. In the script that needs to listen to it, attach new component `IntListener` and attach `Event_new` in `Game Event` field

#### Create Event from scratch

1. Create new data type script (if using custom data type)
2. Create it's corresponding
   - TypeEventSO
   - UnityTypeEvent
   - TypeListener
3. Follow above steps for information on how to use the events

## 1. **Audio**

1. `Event_GameAudioOver`
   - Void Event Type
   - Triggered when game audio is completed.
   - Triggered by `AudioManager`
2. `Event_PlayAudio`
   - Audio Event Type
   - Triggered by scripts that need to play audio.
3. `Event_StopAudio`
   - Int Event Type (_Can be replaced with Audio event type_)
   - Triggered by scripts that need to stop audio.

## 2. **Timer**

1. `Event_StartTimer`
   - Timer Event Type
   - Triggered by other scripts.
2. `Event_StopTimer`
   - Timer Event Type
   - Triggered by other scripts
3. `Event_TimerOver`
   - Timer Event Type
   - Triggered when any type of timer gets over
   - Triggered by `Timer`

## 3. **Others**

1. `Event_LoadScene`
   - Level Load Type
   - Triggered by that need to load levels.
2. `Event_OnCompositionSceneOver`
   - Void Type
   - Triggered when composition scenes get over
   - Triggered by composition controllers.
3. `Event_OnQuestionsReady`
   - Void Type
   - Triggered when questions are ready to be loaded and shown by `BaseUIManager`
   - Triggered by `BaseQuestionManager`

# Quick Jump

1. [Application Overview](../../../../)
2. [Assets Overview](../../../)
3. [Project Overview](../../)
4. [Prefabs Overview](../../Prefabs/)
5. [Scripts Overview](../../Scripts/)
