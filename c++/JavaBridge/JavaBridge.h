/*
 *  JavaBridge.h
 *  JavaBridge
 *
 *  Created by Jhoemar Pagao Dev on 12/18/10.
 *  Copyright 2010 __MyCompanyName__. All rights reserved.
 *
 */

#include <stdlib.h>
#include <JavaVM/jni.h>
//#include <android/log.h>

extern "C"
{
	jint JNI_OnLoad(JavaVM* vm, void* reserved);
	const char* getCacheDir();
}