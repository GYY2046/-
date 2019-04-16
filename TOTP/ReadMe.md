## OTP一次性密码

OTP是One Time Password的简写，即一次性密码。在平时生活中，我们接触一次性密码的场景非常多，比如在登录账号、找回密码，更改密码和转账操作等等这些场景，其中一些常用到的方式有：

1. 手机短信+短信验证码；

2. 邮件+邮件验证码；

3. 认证器软件+验证码，比如Microsoft Authenticator App，Google Authenticator App等等；

4. 硬件+验证码：比如网银的电子密码器；

这些场景的流程一般都是在用户提供了账号+密码的基础上，让用户再提供一个一次性的验证码来提供一层额外的安全防护。通常情况下，这个验证码是一个6-8位的数字，只能使用一次或者仅在很短的时间内可用（比如5分钟以内）。

## 1. HOTP基于消息认证码的一次性密码

HOTP是HMAC-Based One Time Password的缩写，即是基于HMAC（基于Hash的消息认证码）实现的一次性密码。算法细节定义在RFC4226（https://tools.ietf.org/html/rfc4226），算法公式为： HOTP(Key,Counter)  ，拆开是 Truncate(HMAC-SHA-1(Key,Counter)) 。

Key：密钥；

Counter：一个计数器；

HMAC-SHA-1：基于SHA1的HMAC算法的一个函数，返回MAC的值，MAC是一个20bytes（160bits）的字节数组；

Truncate：一个截取数字的函数，以3中的MAC为参数，按照指定规则，得到一个6位或者8位数字（位数太多的话不方便用户输入，太少的话又容易被暴力猜测到）；

## 2. TOTP基于时间的一次性密码

TOTP是Time-Based One Time Password的缩写。TOTP是在HOTP的基础上扩展的一个算法，算法细节定义在RFC6238(https://tools.ietf.org/html/rfc6238)，其核心在于把HOTP中的counter换成了时间T，可以简单的理解为一个当前时间的时间戳（unixtime）。一般实际应用中会固定一个时间的步长，比如30秒，60秒，120秒等等，也就是说再这个步长的时间内，基于TOTP算法算出的OTP值是一样的。

### 参考地址 [一次性密码 && 身份认证三要素](https://www.cnblogs.com/linianhui/p/security-one-time-password.html)