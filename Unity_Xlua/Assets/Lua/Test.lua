print("这里是被调用的")
a= 1
b="haha"

-- 无参数无返回
testFun = function ( )
	print("无参数无返回")
end

-- 有参数有返回
testFun2 = function ( a )
	print("有参数有返回")
	return a + 1
end

-- 多返回
testFun3 = function ( a )
	print("多返回")
	return 1,2,false,"123",a
end

-- 变长参数
testFun4 = function ( a ,...)
	print("变长参数")
	print(a)
	arg = {...}
	for k,v in pairs(arg) do
		print(k.." "..v)
	end
end


