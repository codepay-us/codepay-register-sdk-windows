#ifndef _ERROR_CODE_ECR_H
#define _ERROR_CODE_ECR_H

// Error code definition
#define	ERR_ECR_SUCCESS						0				// Success

#define ERR_ECR_INVALID_PARAMETER			0xE0030001		// Invalid parameter
#define ERR_ECR_LENGTH_OUT_RANGE			0xE0030002		// The length is out of range
#define ERR_ECR_DATA_CHECK					0xE0030005		// Bad data checksum
#define ERR_ECR_BAD_DATA					0xE0030006		// Error in the data 
#define ERR_ECR_BUFFER_NOT_ENOUGH			0xE0030007		// Insufficient buffer size
#define ERR_ECR_TIMEOUT						0xE0030008		// Communication timeout
#define ERR_ECR_READ_DATA					0xE003000A		// Failed to read data
#define ERR_ECR_WRITE_DATA					0xE003000B		// Failed to write data

#define ERR_ECR_USB_CABLE_NOT_CONNECTED		0xE003000F		// No terminal connected to ECR via USB cable
#define ERR_ECR_USB_NOT_CONNECTED			0xE0030010		// No USB connection has been established
#define ERR_ECR_LAN_NOT_CONNECTED			0xE0030011		// No LAN connection has been established
#define ERR_ECR_LAN_SEND_FAILED				0xE0030012		// Failed to send data via LAN
#define ERR_ECR_LAN_CONNECT_FAILED			0xE0030013		// Failed to connect via LAN

#define ERR_ECR_JSON_ERROR					0xE0030014		// Json data incorrect


#endif // _ERROR_CODE_ECR_H


