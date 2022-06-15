The Redis cheat sheet includes basic syntax and methods to help you using Redis. Redis is an open-source (BSD licensed), in-memory data structure store, used as a database, cache, and message broker. It supports data structures such as strings, hashes, lists, sets, sorted sets with range queries, bitmaps, hyperloglogs, geospatial indexes with radius queries and streams.

Redis: Commands Cheat Sheet
The basic syntax of Redis client.

$redis-cli 
Run Commands on the Remote Server

You need to connect to the server by the same client redis-cli

$ redis-cli -h host -p port -a password
Redis: Keys Cheat Sheet
Del Command

redis 127.0.0.1:6379> DEL KEY_NAME
Dump Command

redis 127.0.0.1:6379> DUMP KEY_NAME
Exists Command

redis 127.0.0.1:6379> EXISTS KEY_NAME
Expire Command

redis 127.0.0.1:6379> Expire KEY_NAME TIME_IN_SECONDS
Expireat Command

redis 127.0.0.1:6379> Expireat KEY_NAME TIME_IN_UNIX_TIMESTAMP
Pexpire Command

redis 127.0.0.1:6379> PEXPIRE KEY_NAME TIME_IN_MILLISECONDS
Pexpireat Command

redis 127.0.0.1:6379> PEXPIREAT KEY_NAME TIME_IN_MILLISECONDS_IN_UNIX_TIMESTAMP
Keys Command

redis 127.0.0.1:6379> KEYS PATTERN
Move Command

redis 127.0.0.1:6379> MOVE KEY_NAME DESTINATION_DATABASE
Persist Command

redis 127.0.0.1:6379> PERSIST KEY_NAME
PTTL Command

redis 127.0.0.1:6379> SET tutorialname redis OK
TTL Command

redis 127.0.0.1:6379> TTL KEY_NAME 
Random key Command

redis 127.0.0.1:6379> RANDOMKEY 
Rename Command

redis 127.0.0.1:6379> RENAME OLD_KEY_NAME NEW_KEY_NAME 
Renamenx Command

redis 127.0.0.1:6379> RENAMENX OLD_KEY_NAME NEW_KEY_NAME
Type Command

redis 127.0.0.1:6379> TYPE KEY_NAME 
Redis: Strings Cheat Sheet
Set Command

redis 127.0.0.1:6379> SET KEY_NAME VALUE
Get Command

redis 127.0.0.1:6379> GET KEY_NAME
Getrange Command

redis 127.0.0.1:6379> GETRANGE KEY_NAME start end
Getset Command

redis 127.0.0.1:6379> GETSET KEY_NAME VALUE
Getbit Command

redis 127.0.0.1:6379> GETBIT KEY_NAME OFFSET
Mget Command

redis 127.0.0.1:6379> MGET KEY1 KEY2 .. KEYN
Setbit Command

redis 127.0.0.1:6379> SETBIT KEY_NAME OFFSET
Setex Command

redis 127.0.0.1:6379> SETEX KEY_NAME TIMEOUT VALUE
Setnx Command

redis 127.0.0.1:6379> SETNX KEY_NAME VALUE
Setrange Command

redis 127.0.0.1:6379> SETRANGE KEY_NAME OFFSET VALUE
Strlen Command

redis 127.0.0.1:6379> STRLEN KEY_NAME 
Mset Command

redis 127.0.0.1:6379> MSET key1 value1 key2 value2 .. keyN valueN
Msetnx Command

redis 127.0.0.1:6379> MSETNX key1 value1 key2 value2 .. keyN valueN 
Psetex Command

redis 127.0.0.1:6379> PSETEX key1 EXPIRY_IN_MILLISECONDS value1
Incr Command

redis 127.0.0.1:6379> INCR KEY_NAME
Incrby Command

redis 127.0.0.1:6379> INCRBY KEY_NAME INCR_AMOUNT
Incrbyfloat Command

redis 127.0.0.1:6379> INCRBYFLOAT KEY_NAME INCR_AMOUNT 
Decr Command

redis 127.0.0.1:6379> DECR KEY_NAME
Decrby Command

redis 127.0.0.1:6379> DECRBY KEY_NAME DECREMENT_AMOUNT
Append Command

redis 127.0.0.1:6379> APPEND KEY_NAME NEW_VALUE
Redis: Hashes Cheat Sheet
Hdel Command

redis 127.0.0.1:6379> HDEL KEY_NAME FIELD1.. FIELDN
Hexists Command

redis 127.0.0.1:6379> HEXISTS KEY_NAME FIELD_NAME
Hget Command

redis 127.0.0.1:6379> HGET KEY_NAME FIELD_NAME 
Hgetall Command

redis 127.0.0.1:6379> HGETALL KEY_NAME 
Hincrby Command

redis 127.0.0.1:6379> HINCRBY KEY_NAME FIELD_NAME INCR_BY_NUMBER
Hincrbyfloat Command

redis 127.0.0.1:6379> HINCRBYFLOAT KEY_NAME FIELD_NAME INCR_BY_NUMBER 
Hkeys Command

redis 127.0.0.1:6379> HKEYS KEY_NAME FIELD_NAME INCR_BY_NUMBER 
Hlen Command

redis 127.0.0.1:6379> HLEN KEY_NAME
Hmget Command

redis 127.0.0.1:6379> HMGET KEY_NAME FIELD1...FIELDN 
Hset Command

redis 127.0.0.1:6379> HSET KEY_NAME FIELD VALUE
Hsetnx Command

redis 127.0.0.1:6379> HSETNX KEY_NAME FIELD VALUE
Hvals Command

redis 127.0.0.1:6379> HVALS KEY_NAME FIELD VALUE
Redis: Lists Cheat Sheet
Blpop Command

redis 127.0.0.1:6379> BLPOP LIST1 ... LISTN TIMEOUT
Brpop Command

redis 127.0.0.1:6379> BRPOP LIST1 ... LISTN TIMEOUT
Brpoplpush Command

redis 127.0.0.1:6379> BRPOPLPUSH LIST1 ANOTHER_LIST TIMEOUT 
Lindex Command

redis 127.0.0.1:6379> LINDEX KEY_NAME INDEX_POSITION
Linsert Command

redis 127.0.0.1:6379> LINSERT KEY_NAME BEFORE EXISTING_VALUE NEW_VALUE
Llen Command

redis 127.0.0.1:6379> LLEN KEY_NAME
Lpop Command

redis 127.0.0.1:6379> LPOP KEY_NAME
Lpush Command

redis 127.0.0.1:6379> LPUSH KEY_NAME VALUE1.. VALUEN
Lpushx Command

redis 127.0.0.1:6379> LPUSHX KEY_NAME VALUE1.. VALUEN
Lrange Command

redis 127.0.0.1:6379> LRANGE KEY_NAME START END
Lrem Command

redis 127.0.0.1:6379> LREM KEY_NAME COUNT VALUE 
Lset Command

redis 127.0.0.1:6379> LSET KEY_NAME INDEX VALUE
Ltrim Command

redis 127.0.0.1:6379> LTRIM KEY_NAME START STOP
Rpop Command

redis 127.0.0.1:6379> RPOP KEY_NAME
Rpoplpush Command

redis 127.0.0.1:6379> RPOPLPUSH SOURCE_KEY_NAME DESTINATION_KEY_NAME
Rpush Command

redis 127.0.0.1:6379> RPUSH KEY_NAME VALUE1..VALUEN 
Rpushx Command

redis 127.0.0.1:6379> RPUSHX KEY_NAME VALUE1..VALUEN
Redis: Sets Cheat Sheet
Sadd Command

redis 127.0.0.1:6379> SADD KEY_NAME VALUE1..VALUEN
Scard Command

redis 127.0.0.1:6379> SCARD KEY_NAME
Sdiff Command

redis 127.0.0.1:6379> SDIFF FIRST_KEY OTHER_KEY1..OTHER_KEYN
Sdiffstore Command

redis 127.0.0.1:6379> SDIFFSTORE DESTINATION_KEY KEY1..KEYN 
Sinter Command

redis 127.0.0.1:6379> SINTER KEY KEY1..KEYN
Sinterstore Command

redis 127.0.0.1:6379> SINTERSTORE DESTINATION_KEY KEY KEY1..KEYN
Sismember Command

redis 127.0.0.1:6379> SISMEMBER KEY VALUE
Smove Command

redis 127.0.0.1:6379> SMOVE SOURCE DESTINATION MEMBER
Spop Command

redis 127.0.0.1:6379> SPOP KEY
Srandmember Command

redis 127.0.0.1:6379> SRANDMEMBER KEY [count] 
Srem Command

redis 127.0.0.1:6379> SREM KEY MEMBER1..MEMBERN
Sunion Command

redis 127.0.0.1:6379> SUNION KEY KEY1..KEYN
Sunionstore Command

redis 127.0.0.1:6379> SUNIONSTORE DESTINATION KEY KEY1..KEYN
Sscan Command

redis 127.0.0.1:6379> SSCAN KEY [MATCH pattern] [COUNT count]
Redis: HyperLogLog Cheat Sheet
Pfadd Command
redis 127.0.0.1:6379> PFADD KEY_NAME ELEMENTS_TO_BE_ADDED
Pfcount Command
redis 127.0.0.1:6379> PFCOUNT KEY_NAME KEY1 ... KEYN 
Pfmerge Command
redis 127.0.0.1:6379> PFMERGE KEY_NAME KEY1 ... KEYN
Redis: Publish Subscribe Cheat Sheet
Psubscribe Command
redis 127.0.0.1:6379> PSUBSCRIBE CHANNEL_NAME_OR_PATTERN [PATTERN...]
Pubsub Command
redis 127.0.0.1:6379> PUBSUB subcommand [argument [argument ...]]
Publish Command
redis 127.0.0.1:6379> PUBLISH channel message
Punsubscribe Command
redis 127.0.0.1:6379> PUNSUBSCRIBE [pattern [pattern ...]] 
PubSub Subscribe Command
redis 127.0.0.1:6379> SUBSCRIBE channel [channel ...] 
Unsubscribe Command
redis 127.0.0.1:6379> UNSUBSCRIBE channel [channel ...]
Redis: Transactions Cheat Sheet
Discard Command
redis 127.0.0.1:6379> DISCARD 
Exec Command
redis 127.0.0.1:6379> EXEC
Multi Command
redis 127.0.0.1:6379> MULTI
Unwatch Command
redis 127.0.0.1:6379> UNWATCH
Watch Command
redis 127.0.0.1:6379> WATCH key [key ...]
Redis: Scripting Cheat Sheet
Eval Command
redis 127.0.0.1:6379> EVAL script numkeys key [key ...] arg [arg ...]
Evalsha Command
redis 127.0.0.1:6379> EVALSHA sha1 numkeys key [key ...] arg [arg ...]
Scripting Script Exists Command
redis 127.0.0.1:6379> SCRIPT EXISTS script [script ...]
Scripting Script Flush Command
redis 127.0.0.1:6379> SCRIPT FLUSH 
Scripting Script Kill Command
redis 127.0.0.1:6379> SCRIPT KILL
Scripting Script Load Command
redis 127.0.0.1:6379> SCRIPT LOAD script
Redis: Connections Cheat Sheet
Auth Command
redis 127.0.0.1:6379> AUTH PASSWORD
Echo Command
redis 127.0.0.1:6379> ECHO "SAMPLE_STRING"
Ping Command
redis 127.0.0.1:6379> PING
Quit Command
redis 127.0.0.1:6379> QUIT
Select Command
redis 127.0.0.1:6379> SELECT DB_INDEX