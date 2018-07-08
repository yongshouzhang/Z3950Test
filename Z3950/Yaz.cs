
using System;
using System.Runtime.InteropServices;

namespace Z3950
{
	internal class Yaz
	{
		#region connections

		/// <summary>
		/// create connection, connect to host, if portnum is 0,
		/// then port is read from host string (e.g. myhost:9821) 
		/// </summary>
		/// <param name="host"></param>
		/// <param name="portnum"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_new@8"
			 )]
		public static extern IntPtr ZOOM_connection_new(string host, int portnum);

		/// <summary>
		/// create connection, don't connect, apply options
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_create@4"
			 )]
		public static extern IntPtr ZOOM_connection_create(IntPtr options);

		/// <summary>
		/// connect given existing connection
		/// </summary>
		/// <param name="c"></param>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_connect@12"
			 )]
		public static extern void ZOOM_connection_connect(IntPtr c, string host, int port);

		/// <summary>
		/// destroy connection (close connection also)
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_destroy@4"
			 )]
		public static extern void ZOOM_connection_destroy(IntPtr c);

		/// <summary>
		/// get option for connection
		/// </summary>
		/// <param name="c"></param>
		/// <param name="key"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_option_get@8"
			 )]
		public static extern string ZOOM_connection_option_get(IntPtr c, string key);

		/// <summary>
		/// set option for connection
		/// </summary>
		/// <param name="c"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_option_set@12"
			 )]
		public static extern void ZOOM_connection_option_set(IntPtr c, string key, string value);

		/// <summary>
		/// set option for connection
		/// </summary>
		/// <param name="c"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="length"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_option_setl@16"
			 )]
		public static extern void ZOOM_connection_option_setl(IntPtr c, string key, string value, int length);

		/// <summary>
		/// return error code (0 == success, failure otherwise).
		/// cp holds error string on failure, addinfo holds addititional info (if any)
		/// </summary>
		/// <param name="c"></param>
		/// <param name="cp"></param>
		/// <param name="additionalInfo"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_error@12"
			 )]
		public static extern int ZOOM_connection_error(IntPtr c, string cp, string additionalInfo);


		/// <summary>
		/// return error code (0 == success, failure otherwise).
		/// cp holds error string on failure, addinfo holds addititional info (if any)
		/// </summary>
		/// <param name="c"></param>
		/// <param name="cp"></param>
		/// <param name="additionalInfo"></param>
		/// <param name="diagSet"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_error_x@16"
			 )]
		public static extern int ZOOM_connection_error_x(IntPtr c, string cp, string additionalInfo, string diagSet);

		/// <summary>
		/// returns error code
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_errcode@4"
			 )]
		public static extern int ZOOM_connection_errcode(IntPtr c);

		/// <summary>
		/// returns error message
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_errmsg@4"
			 )]
		public static extern string ZOOM_connection_errmsg(IntPtr c);

		/// <summary>
		/// returns additional info
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_addinfo@4"
			 )]
		public static extern string ZOOM_connection_addinfo(IntPtr c);

		/// <summary>
		/// translates error code into human-readable string
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_diag_str@4"
			 )]
		public static extern string ZOOM_diag_str(int error);

        public const int ZoomErrorNone = 0;
        public const int ZoomErrorConnect = 10000;
        public const int ZoomErrorMemory = 10001;
        public const int ZoomErrorEncode = 10002;
        public const int ZoomErrorDecode = 10003;
        public const int ZoomErrorConnectionLost = 10004;
        public const int ZoomErrorInit = 10005;
        public const int ZoomErrorInternal = 10006;
        public const int ZoomErrorTimeout = 10007;
        public const int ZoomErrorUnsupportedProtocol = 10008;
        public const int ZoomErrorUnsupportedQuery = 10009;
        public const int ZoomErrorInvalidQuery = 10010;

        public const int ZOOM_ERROR_NONE					= 0;
		public const int ZOOM_ERROR_CONNECT					= 10000;
		public const int ZOOM_ERROR_MEMORY					= 10001;
		public const int ZOOM_ERROR_ENCODE					= 10002;
		public const int ZOOM_ERROR_DECODE					= 10003;
		public const int ZOOM_ERROR_CONNECTION_LOST			= 10004;
		public const int ZOOM_ERROR_INIT					= 10005;
		public const int ZOOM_ERROR_INTERNAL				= 10006;
		public const int ZOOM_ERROR_TIMEOUT					= 10007;
		public const int ZOOM_ERROR_UNSUPPORTED_PROTOCOL	= 10008;
		public const int ZOOM_ERROR_UNSUPPORTED_QUERY		= 10009;
		public const int ZOOM_ERROR_INVALID_QUERY			= 10010;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_last_event@4"
			 )]
		public static extern int ZOOM_connection_last_event(IntPtr c);

		public const int ZOOM_EVENT_NONE					= 0;
		public const int ZOOM_EVENT_CONNECT					= 1;
		public const int ZOOM_EVENT_SEND_DATA				= 2;
		public const int ZOOM_EVENT_RECV_DATA				= 3;
		public const int ZOOM_EVENT_TIMEOUT					= 4;
		public const int ZOOM_EVENT_UNKNOWN					= 5;
		public const int ZOOM_EVENT_SEND_APDU				= 6;
		public const int ZOOM_EVENT_RECV_APDU				= 7;
		public const int ZOOM_EVENT_RECV_RECORD				= 8;
		public const int ZOOM_EVENT_RECV_SEARCH				= 9;
	
		#endregion

		#region result sets

		/// <summary>
		/// create result set given a search
		/// </summary>
		/// <param name="c"></param>
		/// <param name="q"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_search@8"
			 )]
		public static extern IntPtr ZOOM_connection_search(IntPtr c, IntPtr q);

		/// <summary>
		/// create result set given PQF query
		/// </summary>
		/// <param name="c"></param>
		/// <param name="q"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_search_pqf@8"
			 )]
		public static extern IntPtr ZOOM_connection_search_pqf(IntPtr c, string q);

		/// <summary>
		/// destroy result set
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_destroy@4"
			 )]
		public static extern IntPtr ZOOM_resultset_destroy(IntPtr r);

		/// <summary>
		/// get result set option
		/// </summary>
		/// <param name="r"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_option_get@8"
			 )]
		public static extern string ZOOM_resultset_option_get(IntPtr r, string key);

		/// <summary>
		/// set result set option
		/// </summary>
		/// <param name="r"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_option_set@12"
			 )]
		public static extern string ZOOM_resultset_option_set(IntPtr r, string key, string value);

		/// <summary>
		/// return size of result set (alias hit count AKA result count)
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_size@4"
			 )]
		public static extern uint ZOOM_resultset_size(IntPtr r);

		/// <summary>
		/// retrieve records
		/// </summary>
		/// <param name="r"></param>
		/// <param name="recs"></param>
		/// <param name="start"></param>
		/// <param name="count"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_records@16"
			 )]
		public static extern void ZOOM_resultset_records(IntPtr r, IntPtr recs, uint start, uint count);

		/// <summary>
		/// return record object at pos. Returns 0 if unavailable
		/// </summary>
		/// <param name="r"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_record@8"
			 )]
		public static extern IntPtr ZOOM_resultset_record(IntPtr r, uint position);

		/// <summary>
		/// like ZOOM_resultset_record - but never blocks
		/// </summary>
		/// <param name="r"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_record_immediate@8"
			 )]
		public static extern IntPtr ZOOM_resultset_record_immediate(IntPtr r, uint position);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_cache_reset@4"
			 )]
		public static extern void ZOOM_resultset_cache_reset(IntPtr r);

		#endregion

		#region records

		/// <summary>
		/// get record information, in a form given by type
		/// </summary>
		/// <param name="r"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_record_get@12"
			 )]
		public static extern string ZOOM_record_get_string(IntPtr record, string type, ref int length);

		/// <summary>
		/// get record information, in a form given by type
		/// </summary>
		/// <param name="r"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_record_get@12"
			 )]
		private static extern IntPtr ZOOM_record_get_IntPtr(IntPtr record, string type, ref int length);

		public static unsafe byte[] ZOOM_record_get_bytes(IntPtr record, string type, ref int length)
		{
			IntPtr ipRecord = ZOOM_record_get_IntPtr(record, type, ref length);
			byte* pRecord = (byte*) ipRecord.ToPointer();
			byte[] recordBytes = new byte[length];
			for (int i = 0; i < length; i++)
			{
				recordBytes[i] = *pRecord;
				pRecord++;
			}

			return recordBytes;
		}

		/// <summary>
		/// destroy record
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_record_destroy@4"
			 )]
		public static extern string ZOOM_record_destroy(IntPtr record);

		/// <summary>
		/// return copy of record
		/// </summary>
		/// <param name="sourceRecord"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_record_clone@4"
			 )]
		public static extern IntPtr ZOOM_record_clone(IntPtr sourceRecord);

		#endregion

		#region queries

		/// <summary>
		/// create search object
		/// </summary>
		/// <param name="sourceRecord"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_query_create@0"
			 )]
		public static extern IntPtr ZOOM_query_create();

		/// <summary>
		/// destroy it
		/// </summary>
		/// <param name="s"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_query_destroy@4"
			 )]
		public static extern void ZOOM_query_destroy(IntPtr s);

		/// <summary>
		/// CQL
		/// </summary>
		/// <param name="s"></param>
		/// <param name="query"></param>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_query_cql@8"
			 )]
		public static extern int ZOOM_query_cql(IntPtr s, string query);

		/// <summary>
		/// PQF
		/// </summary>
		/// <param name="s"></param>
		/// <param name="query"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_query_prefix@8"
			 )]
		public static extern int ZOOM_query_prefix(IntPtr s, string query);

		/// <summary>
		/// specify sort criteria for search
		/// </summary>
		/// <param name="s"></param>
		/// <param name="criteria"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_query_sortby@8"
			 )]
		public static extern int ZOOM_query_sortby(IntPtr s, string criteria);

		#endregion

		#region scan

		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <param name="startterm"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_scan@8"
			 )]
		public static extern IntPtr ZOOM_connection_scan(IntPtr connection, string startterm);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_term@16"
			 )]
		public static extern string ZOOM_scanset_term(IntPtr scan, uint position, out int occ, out int length);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_display_term@16"
			 )]
		public static extern string ZOOM_scanset_display_term(IntPtr scan, uint position, int occ, int length);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_size@4"
			 )]
		public static extern uint ZOOM_scanset_size(IntPtr scan);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_destroy@4"
			 )]
		public static extern uint ZOOM_scanset_destroy(IntPtr scan);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_option_get@8"
			 )]
		public static extern uint ZOOM_scanset_option_get(IntPtr scan, string key);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_scanset_option_set@12"
			 )]
		public static extern uint ZOOM_scanset_option_set(IntPtr scan, string key, string value);

		#endregion

		#region Extended Services Packages

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_connection_package@8"
			 )]
		public static extern IntPtr ZOOM_connection_package(IntPtr connection, IntPtr options);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_package_destroy@4"
			 )]
		public static extern void ZOOM_package_destroy(IntPtr package);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_package_send@8"
			 )]
		public static extern void ZOOM_package_send(IntPtr package, string type);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_package_option_get@8"
			 )]
		public static extern string ZOOM_package_option_get(IntPtr package, string key);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_package_option_set@12"
			 )]
		public static extern void ZOOM_package_option_set(IntPtr package, string key, string value);

		#endregion

		#region sort

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_resultset_sort@12"
			 )]
		public static extern void ZOOM_resultset_sort(IntPtr resultSet, string sortType, string sortSpec);

		#endregion

		#region options

		/* This area uses callbacks and I can't see any need for them - RS, 25/04/2005
			[DllImport("yaz.dll",
				SetLastError=true,
				CharSet=CharSet.Ansi,
				CallingConvention=CallingConvention.Cdecl,
				EntryPoint="_ZOOM_options_set_callback@12"
				)]
			public static extern IntPtr ZOOM_options_set_callback(IntPtr options, YazCallback callback );

			public delegate void YazCallback(IntPtr handle, string name);

			ZOOM_API(ZOOM_options_callback)
				ZOOM_options_set_callback (ZOOM_options opt,
					ZOOM_options_callback c,
					void *handle);
		*/

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_create@0"
			 )]
		public static extern IntPtr ZOOM_options_create();

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_create_with_parent@4"
			 )]
		public static extern IntPtr ZOOM_options_create_with_parent(IntPtr parentOptions);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_create_with_parent2@8"
			 )]
		public static extern IntPtr ZOOM_options_create_with_parent2(IntPtr parentOptions1, IntPtr parentOptions2);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_get@8"
			 )]
		public static extern string ZOOM_options_get(IntPtr options, string key);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_set@12"
			 )]
		public static extern void ZOOM_options_set(IntPtr options, string key, string value);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_setl@16"
			 )]
		public static extern void ZOOM_options_setl(IntPtr options, string key, string value, int length);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_destroy@4"
			 )]
		public static extern void ZOOM_options_destroy(IntPtr options);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_get_bool@12"
			 )]
		public static extern int ZOOM_options_get_bool(IntPtr options, string name, int defa);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_get_int@12"
			 )]
		public static extern int ZOOM_options_get_int(IntPtr options, string name, int defa);

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_set_int@12"
			 )]
		public static extern void ZOOM_options_set_int(IntPtr options, string name, int defa);

		#endregion

		#region events

		/// <summary>
		/// poll for events on a number of connections. Returns positive
		/// integer if event occurred ; zero if none occurred and no more
		/// events are pending. The positive integer specifies the
		/// connection for which the event occurred.
		/// </summary>
		/// <param name="no"></param>
		/// <param name="connection"></param>
		/// <returns></returns>
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="_ZOOM_options_set_int@12"
			 )]
		public static extern int ZOOM_event(int no, IntPtr connection);

		#endregion

		#region Yaz Functions

		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="yaz_log_init"
			 )]
		public static extern void yaz_log_init(int level, string prefix, string name);
		
		[DllImport("yaz.dll",
			 SetLastError=true,
			 CharSet=CharSet.Ansi,
			 CallingConvention=CallingConvention.Cdecl,
			 EntryPoint="yaz_log"
			 )]
		public static extern void yaz_log(LogLevel level, string message);

		public enum LogLevel
		{
			FATAL = 0x00000001,
			DEBUG = 0x00000002,
			WARN  = 0x00000004,
			LOG   = 0x00000008
		}
		
		#endregion
	}
}
